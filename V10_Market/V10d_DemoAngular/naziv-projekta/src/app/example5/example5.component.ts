import {ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit} from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

export interface CreateProductCommand {
  name: string;
  description: string;
  price: number;
  categoryId: number;
}

export interface ListProductCategoryQueryResponse {
  items: ListProductCategoryQueryDto[];
  pageSize: number;
  currentPage: number;
  includedTotal: boolean;
  totalItems: number;
  totalPages: number;
}

export interface ListProductCategoryQueryDto {
  id: number;
  name: string;
  isEnabled: boolean;
}

@Component({
  selector: 'app-example5',
  standalone: false,
  templateUrl: './example5.component.html',
  styleUrl: './example5.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Example5Component implements OnInit {
  JWT_TOKEN =
    'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiI0IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoic3RyaW5nIiwiaXNfYWRtaW4iOiJmYWxzZSIsImlzX21hbmFnZXIiOiJmYWxzZSIsImlzX2VtcGxveWVlIjoidHJ1ZSIsInZlciI6IjAiLCJpYXQiOjE3NjUyNDU5ODAsImp0aSI6IjhjYmExYmMxZThmMDRhNWY4ODBhZDRhMDJkNTU1MjBmIiwiYXVkIjpbIk1hcmtldC5TcGEiLCJNYXJrZXQuU3BhIl0sIm5iZiI6MTc2NTI0NTk4MCwiZXhwIjoxNzY1MjQ2ODgwLCJpc3MiOiJNYXJrZXQuQXBpIn0.PfHpI0yuN4muTIZuKZMxhVos_fY23CzAqg7GeVvjd6Y';

  API_BASE = 'https://localhost:7260';

  // for showing backend response
  responseInfo: string = '...';

  // list of categories for the select
  categoryData: ListProductCategoryQueryDto[] = [];

  // reactive form
  productForm: FormGroup;

  constructor(
    private http: HttpClient,
    private fb: FormBuilder,
    private cdr: ChangeDetectorRef
  ) {
    this.productForm = this.fb.group({
      name: [
        'neko ime ' + new Date().toLocaleDateString('en-US'),
        [Validators.required, Validators.maxLength(100)]
      ],
      description: [''],
      price: [1, [Validators.required, Validators.min(0.01)]],
      categoryId: [1, Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.JWT_TOKEN}`
    });

    this.http
      .get<ListProductCategoryQueryResponse>(
        `${this.API_BASE}/ProductCategories`,
        { headers }
      )
      .subscribe({
        next: (res) => {
          this.categoryData = res.items;
          this.cdr.markForCheck();
        },
        error: (err) => {
          console.error(err);
          this.responseInfo = 'Error loading categories: ' + err;
          this.cdr.markForCheck(); // DODAJ OVO
        }
      });
  }

  submitProduct(): void {
    if (this.productForm.invalid) {
      this.productForm.markAllAsTouched();
      return;
    }

    const command: CreateProductCommand = this.productForm.value;

    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.JWT_TOKEN}`,
      'Content-Type': 'application/json'
    });

    this.http
      .post(`${this.API_BASE}/Products`, command, { headers })
      .subscribe({
        next: (res) => {
          this.responseInfo = JSON.stringify(res);
          console.log('Success!', res);
        },
        error: (err) => {
          console.error(err);
          this.responseInfo = 'Error: ' + err;
        }
      });
  }

  getControl(name: string) {
    return this.productForm.get(name);
  }
}
