import { HttpClient, HttpHeaders } from '@angular/common/http';
import {ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit} from '@angular/core';

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
  selector: 'app-example4',
  standalone: false,
  templateUrl: './example4.component.html',
  styleUrl: './example4.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Example4Component implements OnInit {

  JWT_TOKEN =
"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiI0IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoic3RyaW5nIiwiaXNfYWRtaW4iOiJmYWxzZSIsImlzX21hbmFnZXIiOiJmYWxzZSIsImlzX2VtcGxveWVlIjoidHJ1ZSIsInZlciI6IjAiLCJpYXQiOjE3NjUyNzc5MDgsImp0aSI6ImVkZTA2MTBlNjllNjQyMWZiOGI1MmFhOTUzY2JmZDI5IiwiYXVkIjpbIk1hcmtldC5TcGEiLCJNYXJrZXQuU3BhIl0sIm5iZiI6MTc2NTI3NzkwOCwiZXhwIjoxNzY1Mjc4ODA4LCJpc3MiOiJNYXJrZXQuQXBpIn0.KVsFX9o4f30q7rKC8OSk5ISNdE5B2fDsHdS50ZjRMWA";
  API_BASE = "https://localhost:7260";

  responseInfo = "...";

  categoryData: ListProductCategoryQueryDto[] = [];

  productCommand: CreateProductCommand = {
    name: "neko ime " + new Date().toLocaleDateString("en-US"),
    description: "",
    price: 1,
    categoryId: 1,
  };

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.loadCategories();
  }

  loadCategories() {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.JWT_TOKEN}`,
    });

    this.http
      .get<ListProductCategoryQueryResponse>(
        `${this.API_BASE}/ProductCategories`,
        { headers }
      )
      .subscribe({
        next: (res) => {
          this.categoryData = res.items;
          //this.cd.markForCheck();
        },
        error: (err) => {
          console.error(err);
        }
      });
  }

  submitProduct(event: Event) {
    event.preventDefault();

    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.JWT_TOKEN}`,
      "Content-Type": "application/json",
    });

    this.http
      .post(`${this.API_BASE}/Products`, this.productCommand, { headers })
      .subscribe({
        next: (res) =>{
          this.responseInfo = JSON.stringify(res);
          },
        error: (err) => {
          this.responseInfo = err.toString();
        }
      });
  }
}
