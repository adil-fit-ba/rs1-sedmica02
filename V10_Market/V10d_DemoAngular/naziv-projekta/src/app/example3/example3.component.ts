import {ChangeDetectorRef, Component, OnInit} from '@angular/core';

export interface CreateProductCommand {
  name: string
  description: string
  price: number
  categoryId: number
}


export interface ListProductCategoryQueryResponse {
  items: ListProductCategoryQueryDto[]
  pageSize: number
  currentPage: number
  includedTotal: boolean
  totalItems: number
  totalPages: number
}

export interface ListProductCategoryQueryDto {
  id: number
  name: string
  isEnabled: boolean
}


@Component({
  selector: 'app-example3',
  standalone: false,
  templateUrl: './example3.component.html',
  styleUrl: './example3.component.scss',
})
export class Example3Component implements OnInit {
  JWT_TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiI0IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoic3RyaW5nIiwiaXNfYWRtaW4iOiJmYWxzZSIsImlzX21hbmFnZXIiOiJmYWxzZSIsImlzX2VtcGxveWVlIjoidHJ1ZSIsInZlciI6IjAiLCJpYXQiOjE3NjUyNDU5ODAsImp0aSI6IjhjYmExYmMxZThmMDRhNWY4ODBhZDRhMDJkNTU1MjBmIiwiYXVkIjpbIk1hcmtldC5TcGEiLCJNYXJrZXQuU3BhIl0sIm5iZiI6MTc2NTI0NTk4MCwiZXhwIjoxNzY1MjQ2ODgwLCJpc3MiOiJNYXJrZXQuQXBpIn0.PfHpI0yuN4muTIZuKZMxhVos_fY23CzAqg7GeVvjd6Y";
  API_BASE = "https://localhost:7260";
  responseInfo = "...";

  constructor(private cd: ChangeDetectorRef) {
  }

  productCommand: CreateProductCommand = {
    name : "neko ime " + new Date().toLocaleDateString("en-US"),
    description : "",
    price : 1,
    categoryId : 1
  }
  public categoryData: ListProductCategoryQueryDto[] = [];

  ngOnInit() {
    this.loadCategories();
  }

  async loadCategories() {
    try {
      const response = await fetch(`${this.API_BASE}/ProductCategories`, {
        headers: {
          "Authorization": `Bearer ${this.JWT_TOKEN}`
        }
      });

      if (!response.ok) {
        throw new Error("Failed to load categories: " + response.status);
      }

      let data: ListProductCategoryQueryResponse = await response.json();
      this.categoryData = data.items;
      this.cd.markForCheck();
    } catch (err: any) {
      document.getElementById("result")!.textContent = err.toString();
      console.error(err);
    }
  }

  submitProduct(event: any) {
    event.preventDefault();

    fetch(`${this.API_BASE}/Products`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${this.JWT_TOKEN}`
      },
      body: JSON.stringify(this.productCommand)
    })
      .then(response => response.json())
      .then(response => {
        console.log("Success!");
        this.responseInfo = response;
      })
      .catch(error => {
        console.log(error);
        this.responseInfo = error;
      });

  }
}
