import {Component, inject, OnInit} from '@angular/core';
import {ProductCategoriesApiService} from '../../../../api-services/product-categories/product-categories-api.service';
import {ListProductCategoriesQueryDto} from '../../../../api-services/product-categories/product-categories-api.model';
import {DialogHelperService} from '../../../shared/services/dialog-helper.service';
import {DialogButton} from '../../../shared/models/dialog-config.model';
import {ToasterService} from '../../../../core/services/toaster.service';

@Component({
  selector: 'app-product-categories-2',
  standalone: false,
  templateUrl: './product-categories-2.component.html',
  styleUrl: './product-categories-2.component.scss',
})
export class ProductCategories2Component implements OnInit {
  private apiService = inject(ProductCategoriesApiService);
  private dialogHelper = inject(DialogHelperService);
  private toaster = inject(ToasterService);

  public productCategoriesList: ListProductCategoriesQueryDto[] = [];


  ngOnInit(): void {
    this.apiService.list({
      paging:{
        page:1,
        pageSize: 1000
      }
    }).subscribe({
      next: (response) => {
        this.productCategoriesList = response.items;
        this.toaster.success("ok su podaci: " + this.productCategoriesList.length);
      },
      error: (error) => {
        this.toaster.error(error);
      }
    });
  }


  changeStatus(x: ListProductCategoriesQueryDto, $event: Event) {



    if (x.isEnabled){
      this.apiService.disable(x.id).subscribe({
        next: response =>{
          this.toaster.success("update je uspješan: ");
          this.ngOnInit();
        },
        error: (error) => {
          this.toaster.error(error);
          this.ngOnInit();
        }
      })
    }
    else{
      this.apiService.enable(x.id).subscribe(
        {
          next: response =>{
            this.toaster.success("update je uspješan: ");
            this.ngOnInit();
          },
          error: (error) => {
            this.toaster.error(error);
            this.ngOnInit();
          }
        }
      )
    }
  }

  deleteAction(x: ListProductCategoriesQueryDto) {
    this.dialogHelper.productCategory.confirmDelete(x.name).subscribe((result) => {
      if (result && result.button === DialogButton.DELETE) {
        this.apiService.delete(x.id).subscribe({
          next: response =>{
            this.ngOnInit();
            this.toaster.success(`Brisanje ${x.name} uspješno`)
          },
          error: (error) => {
            this.toaster.error(error);
            this.ngOnInit();
          }
        })
      }
      }

    );
  }
}
