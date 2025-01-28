# Project Documentation

**Ticket Management Application**  
A  application or managing tickets with the ability to add, edit, delete, and purchase tickets. The application also includes functionality for user registration and login, allowing users to create accounts, securely log in, and manage their ticket purchases.

# API Documentation for AccountController
## 1. User Registration

### **Endpoint:** `/register`

**Method:** `POST`  
**Authentication:** Not Required  
**Description:** Creates a new user account and logs them in automatically if registration is successful.
Request Body:
| Parameter | Type |Description|Required|
|--|--|--|--|--|
|Email|String|User's email address (also username)|Yes
|Password| String |User's password|Yes|

### **Response:**

#### **201 Created**

On success, returns a success message and redirects to the welcome page.

#### **400 Bad Request**

If validation fails, returns error messages.


## 2. Render Registration Page

### **Endpoint:** `/register`

**Method:** `GET`  
**Authentication:** Not Required  
**Description:** Returns the registration form view.

## 3. User Login

### **Endpoint:** `/login`

**Method:** `POST`  
**Authentication:** Not Required  
**Description:** Authenticates a user using their email and password.
| Parameter | Type |Description|Required|
|--|--|--|--|
|Email|String|User's email address (username)|yes
|Password| String |User's password|Yes|

### **Response:**

#### **200 OK**

On success, redirects to the `Ticket` page or the specified return URL.

#### **401 Unauthorized**

If credentials are incorrect, returns an error message.


## 4. Render Login Page

### **Endpoint:** `/login`

**Method:** `GET`  
**Authentication:** Not Required  
**Description:** Returns the login form view.


## 5. User Logout

### **Endpoint:** `/logout`

**Method:** `POST`  
**Authentication:** Required  
**Description:** Logs out the authenticated user.

### **Response:**

#### **200 OK**

On success, logs out the user and redirects to the login page.


# API Documentation for TicketController

## 1. Get All Tickets

### **Endpoint:** `/tickets`

**Method:** `GET`  
**Authentication:** Required  
**Description:** Fetches a list of all available tickets.

### **Response:**

#### **200 OK**

Returns a list of all tickets.
## 2. Create a New Ticket

### **Endpoint:** `/tickets/create`

**Method:** `POST`  
**Authentication:** Required (Admin Only)  
**Description:** Creates a new ticket.

### **Request Body:**
| Parameter | Type |Description|Required|
|--|--|--|--|
|Name|String|Ticket name|yes
|Description| String |Ticket description|No|
|Price| Decimal|Ticket description|Yes|
|AvailableTickets| Int|Number of available tickets|Yes|

### **Response:**

#### **201 Created**

Returns the created ticket.

#### **400 Bad Request**

Validation errors if data is incorrect.

## 3. Get Ticket by ID

### **Endpoint:** `/tickets/{id}`

**Method:** `GET`  
**Authentication:** Required  
**Description:** Fetches details of a specific ticket by ID.

### **Response:**

#### **200 OK**

Returns the requested ticket.

#### **404 Not Found**

If the ticket does not exist.

## 4. Update a Ticket

### **Endpoint:** `/tickets/{id}/edit`

**Method:** `PUT`  
**Authentication:** Required (Admin Only)  
**Description:** Updates an existing ticket.

### **Request Body:**
| Parameter | Type |Description|Required|
|--|--|--|--|
|Name|String|Ticket name|yes
|Description| String |Ticket description|No|
|Price| Decimal|Ticket description|Yes|
|AvailableTickets| Int|Number of available tickets|Yes|
### **Response:**

#### **200 OK**

Returns the updated ticket.

#### **404 Not Found**

If the ticket does not exist.

#### **400 Bad Request**

Validation errors.

## 5. Delete a Ticket

### **Endpoint:** `/tickets/{id}/delete`

**Method:** `DELETE`  
**Authentication:** Required (Admin Only)  
**Description:** Deletes a specific ticket by ID.

### **Response:**

#### **200 OK**

Ticket deleted successfully.

#### **404 Not Found**

If the ticket does not exist.

## 6. Purchase Ticket

### **Endpoint:** `/tickets/{id}/purchase`

**Method:** `POST`  
**Authentication:** Required  
**Description:** Purchases a ticket for the logged-in user.

### **Request Body:**

| Parameter | Type |Description|Required|
|--|--|--|--|
|Quantity|Int|Number of tickets to purchase name|yes
### **Response:**

#### **200 OK**

Purchase successful.

#### **400 Bad Request**

If not enough tickets are available.

#### **404 Not Found**

If the ticket does not exist.

Oto dokumentacja API dla `TicketController`:

----------


## 7. Get User's Purchased Tickets

### **Endpoint:** `/tickets/my-tickets`

**Method:** `GET`  
**Authentication:** Required  
**Description:** Fetches the list of tickets purchased by the logged-in user.

### **Response:**

#### **200 OK**

Returns the list of purchased tickets.

## 8. View Purchase Page

### **Endpoint:** `/tickets/{id}/purchase`

**Method:** `GET`  
**Authentication:** Required  
**Description:** Returns the purchase form for a specific ticket.

#### **Response:**

#### **200 OK**

Returns the purchase page with ticket details.

#### **404 Not Found**

If the ticket does not exist.
# API Documentation for HomeController

## 1. Welcome Page

### **Endpoint:**  `/welcome`

**Method:**  `GET`  
**Authentication:** Not Required  
**Description:** Returns the welcome page view.

### **Response:**

#### **200 OK**

Returns the welcome page.

## 2. Home Page

### **Endpoint:**  `/`

**Method:**  `GET`  
**Authentication:** Not Required  
**Description:** Returns the home page view.

### **Response:**

#### **200 OK**

Returns the home page.

# API Documentation for DashboardController

## 1. Dashboard Page

### **Endpoint:**  `/api/dashboard`

**Method:**  `GET`  
**Authentication:** Required  
**Description:** Returns the main dashboard view for authenticated users.

### **Response:**

#### **200 OK**

Returns the dashboard page.

#### **401 Unauthorized**

If the user is not authenticated, access is denied.

## Database Structure

The application uses an SQL database to manage its data. Below is a description of the database structure, including all tables, their columns, and relationships.

### Tables

#### 1. Users

This table stores information about users for authentication and authorization.

| Column Name | Data Type |Constraints|Description|
|--|--|--|--|
|Email|VARCHAR(255)|Primary Key, Not Null,Unique|Email address of the user. Used as a unique identifier.
|Password| VARCHAR(255)|Not Null|Hashed password for authentication.|

#### 2. Tickets

This table stores information about event tickets.
| Column Name | Data Type |Constraints|Description|
|--|--|--|--|
|ID|INT|Primary Key, Auto-Increment|Unique identifier for each ticket.
|Name| VARCHAR(255)|Not Null|Name of the event related to the ticket.|
|AvailableTickets| INT|Not Null|Name of the event related to the ticket.|
|Price| DECIMAL(10,2)|Not Null|Price of a single ticket.|

#### 3. Purchases

This table stores details about ticket purchases.

| Column Name | Data Type |Constraints|Description|
|--|--|--|--|
|ID|INT|Primary Key, Auto-Increment|Unique identifier for each purchase.
|TicketId| INT|Foreign Key (References Tickets(ID)), Not Null|Ticket associated with the purchase.|
|PurchaseDate| DATETIME|Not Null|Date and time when the purchase was made.|
|Quantity| INT(10,2)|Not Null|Number of tickets purchased.|
|TotalPrice| DECIMAL(10,2)|Not Null|Total price for the purchase.|
|UserId|VARCHAR(255)|Foreign Key (References Users(Email)), Not Null|User who made the purchase.|

### Relationships

1.  **Users & Purchases**: The `Purchases` table references the `Users` table through the `UserId` column. This ensures that each purchase is associated with a valid user.
    
2.  **Tickets & Purchases**: The `Purchases` table references the `Tickets` table through the `TicketId` column. This maintains referential integrity, ensuring that each purchase corresponds to an existing ticket.
    

### Authentication Models

Besides the core database structure, the application includes the following authentication models:

-   **LoginViewModel**: Used to authenticate users with an email and password.
    
-   **RegisterViewModel**: Used for user registration, requiring an email, password, and password confirmation.
    

These models facilitate authentication but do not directly affect the database schema, as they are used in the application logic rather than being stored in the database directly.

This database design ensures efficient management of user authentication, event tickets, and purchase transactions while maintaining data integrity through relational constraints.
