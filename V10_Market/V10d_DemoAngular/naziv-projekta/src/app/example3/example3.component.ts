import {Component, OnInit} from '@angular/core';

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
  JWT_TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIzIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiIzIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoic3RyaW5nIiwiaXNfYWRtaW4iOiJmYWxzZSIsImlzX21hbmFnZXIiOiJmYWxzZSIsImlzX2VtcGxveWVlIjoidHJ1ZSIsInZlciI6IjAiLCJpYXQiOjE3NjQ2NzcwMTQsImp0aSI6ImRiMmIzOTMxMGE1NzQwY2NhZDA5ZTIxMTBkNWZiY2M4IiwiYXVkIjpbIk1hcmtldC5TcGEiLCJNYXJrZXQuU3BhIl0sIm5iZiI6MTc2NDY3NzAxNCwiZXhwIjoxNzY0Njg3ODE0LCJpc3MiOiJNYXJrZXQuQXBpIn0.S4PNB_RZd7D88hPRI0xAed-fqMPy0GtD5hEQMOcVERs";
  API_BASE = "https://localhost:7260";
  responseInfo = "...";

  productCommand: CreateProductCommand = {
    name : "neko ime " + new Date().toLocaleDateString("en-US"),
    description : "",
    price : 1,
    categoryId : 1
  }

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

      const data:ListProductCategoryQueryResponse = await response.json();
      const select: any = document.getElementById("category");

      // API returns { total, items: [ { id, name, isEnabled } ] }
      (data.items || []).forEach((cat: any) => {
        const option = document.createElement("option");
        option.value = cat.id;
        option.textContent = cat.name;
        select.appendChild(option);
      });
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
