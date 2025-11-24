# 🚀 Express + Swagger – Uputstvo za pokretanje

Ovaj primjer prikazuje jednostavan Node.js + Express REST API sa Swagger dokumentacijom.

---

## 📌 1. Instalacija paketa

U folderu gdje se nalazi `example2.js` pokreni:

```bash
npm install
```

Time se instaliraju svi paketi definirani u `package.json` (Express, Swagger UI, Swagger JSDoc, itd.)

---

## 📌 2. Pokretanje servera

U istom folderu:

```bash
node example2.js
```

Ako je sve ispravno, u terminalu ćeš vidjeti:

```
REST API server running on port 8080
Swagger docs: http://localhost:8080/swagger
```

---

## 📌 3. Otvori Swagger dokumentaciju

U browseru:

👉 **http://localhost:8080/swagger**

Tu ćeš moći:
- Pregledati sve dostupne API rute
- Testirati ih direktno iz browsera
- Vidjeti opis parametara i odgovora

---

## 📌 4. Primjer API poziva

### GET svi korisnici
```
http://localhost:8080/api/users
```

### GET korisnik po ID-u
```
http://localhost:8080/api/users/1
```

---

## 🛠️ Tehnologije

* **Node.js** – Runtime okruženje
* **Express.js** – Web framework
* **Swagger UI Express** – Vizualizacija API dokumentacije
* **Swagger JSDoc** – Automatska generacija dokumentacije iz komentara

---

## 📝 Napomene

* Server radi na **portu 8080**
* Swagger dokumentacija je dostupna na `/swagger` ruti
* API rute počinju sa `/api/`
* Prije pokretanja, obavezno pokreni `npm install`