# 🚀 Express API – Uputstvo za pokretanje

Ovaj projekat koristi Node.js i Express za pokretanje jednostavnog REST API-ja.

## 📌 1. Instalacija Express-a (obavezno)

U folderu gdje se nalazi `example1.js` otvori terminal i pokreni:

```bash
npm init -y
npm install express
```

### Šta ove komande rade?

* `npm init -y` – Automatski kreira `package.json` fajl.
* `npm install express` – Instalira Express paket i kreira `node_modules` direktorij.

## 📌 2. Pokretanje servera

U istom folderu pokreni:

```bash
node example1.js
```

Ako je sve ispravno, vidjet ćeš:

```
REST API server running on port 8080
```

## 📌 3. Testiranje API-ja

Možeš testirati API u browseru, Postmanu ili VS Code REST Clientu.

### Dostupne rute:

* **GET** svi korisnici 👉 `http://localhost:8080/api/users`
* **GET** korisnik po ID-u 👉 `http://localhost:8080/api/users/1`

---

## 🛠️ Tehnologije

* Node.js
* Express.js

## 📝 Napomene

* Server radi na **portu 8080**