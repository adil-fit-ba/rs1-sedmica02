const express = require('express');
const app = express();

app.use(express.json());

// ===============================================
// SWAGGER SETUP
// ===============================================
const swaggerUi = require('swagger-ui-express');
const swaggerJsdoc = require('swagger-jsdoc');

const swaggerOptions = {
  definition: {
    openapi: "3.0.0",
    info: {
      title: "User API (Express + Swagger)",
      version: "1.0.0",
    },
  },
  apis: [__filename], // <-- NAJSIGURNIJA OPCIJA!
};

const swaggerSpec = swaggerJsdoc(swaggerOptions);
app.use("/swagger", swaggerUi.serve, swaggerUi.setup(swaggerSpec));


// ===============================================
// USERS API
// ===============================================

let users = [
  { id: 1, name: 'John Doe', email: 'john@example.com' },
  { id: 2, name: 'Jane Smith', email: 'jane@example.com' }
];

/**
 * @openapi
 * /api/users:
 *   get:
 *     summary: Get all users
 *     responses:
 *       200:
 *         description: List of users
 */
app.get('/api/users', (req, res) => {
  res.json(users);
});

/**
 * @openapi
 * /api/users/{id}:
 *   get:
 *     summary: Get a user by ID
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: integer
 *     responses:
 *       200:
 *         description: One user
 *       404:
 *         description: User not found
 */
app.get('/api/users/:id', (req, res) => {
  const user = users.find(u => u.id === parseInt(req.params.id));
  if (!user) return res.status(404).json({ message: 'User not found' });
  res.json(user);
});


app.listen(8080, () => {
  console.log("REST API server running on port 8080");
  console.log("Swagger docs: http://localhost:8080/swagger");
});
