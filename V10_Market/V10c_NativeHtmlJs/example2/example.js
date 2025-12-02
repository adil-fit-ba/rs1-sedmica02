    const JWT_TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIzIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiIzIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoic3RyaW5nIiwiaXNfYWRtaW4iOiJmYWxzZSIsImlzX21hbmFnZXIiOiJmYWxzZSIsImlzX2VtcGxveWVlIjoidHJ1ZSIsInZlciI6IjAiLCJpYXQiOjE3NjM5ODEyNTcsImp0aSI6IjhhOWIwOTQyYzAwNDQwYTY5NTBjNDExYTY2NWUzZDJjIiwiYXVkIjpbIk1hcmtldC5TcGEiLCJNYXJrZXQuU3BhIl0sIm5iZiI6MTc2Mzk4MTI1NywiZXhwIjoxNzYzOTkyMDU3LCJpc3MiOiJNYXJrZXQuQXBpIn0.VLF08JDlcQmDNMxPm1pJUYEBkucKS6-IIHCcAuH6I-c";

    const API_BASE = "https://localhost:7260";

    async function loadCategories() {
        try {
            const response = await fetch(`${API_BASE}/ProductCategories`, {
                headers: {
                    "Authorization": `Bearer ${JWT_TOKEN}`
                }
            });

            if (!response.ok) {
                throw new Error("Failed to load categories: " + response.status);
            }

            const data = await response.json();
            const select = document.getElementById("category");

            // API returns { total, items: [ { id, name, isEnabled } ] }
            (data.items || []).forEach(cat => {
                const option = document.createElement("option");
                option.value = cat.id;
                option.textContent = cat.name;
                select.appendChild(option);
            });
        } catch (err) {
            document.getElementById("result").textContent = err.toString();
            console.error(err);
        }
    }

    async function submitProduct(event) {
        event.preventDefault();

        const name = document.getElementById("name").value;
        const description = document.getElementById("description").value;
        const price = parseFloat(document.getElementById("price").value);
        const categoryId = parseInt(document.getElementById("category").value, 10);

        const payload = {
            name: name,
            description: description,
            price: price,
            categoryId: categoryId
        };

        try {
            const response = await fetch(`${API_BASE}/Products`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${JWT_TOKEN}`
                },
                body: JSON.stringify(payload)
            });

            const text = await response.text();
            document.getElementById("result").textContent =
                "Status: " + response.status + "\n\n" + text;
        } catch (err) {
            document.getElementById("result").textContent = err.toString();
            console.error(err);
        }
    }

    document
        .getElementById("productForm")
        .addEventListener("submit", submitProduct);

    // load categories on page load
    loadCategories();