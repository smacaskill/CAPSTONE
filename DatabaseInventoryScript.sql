USE FPS_INVENTORY

CREATE TABLE CATEGORY(
	IDCATEGORY INT PRIMARY KEY IDENTITY(1,1),
	CATEGORY_NAME VARCHAR(30) NOT NULL,
	CATEGORY_CREATION DATE NOT NULL DEFAULT GETDATE()
)
GO

CREATE TABLE SUBCATEGORY(
	IDSUBCATEGORY INT PRIMARY KEY IDENTITY(1,1),
	SUBCATEGORY_NAME VARCHAR(30) NOT NULL,
	SUBCATEGORY_CREATION DATE NOT NULL DEFAULT GETDATE(),
	ID_CATEGORY INT --CATEGORY FK
)
GO

CREATE TABLE BRAND(
	IDBRAND INT PRIMARY KEY IDENTITY(1,1),
	BRAND_NAME VARCHAR(30) NOT NULL,
	BRAND_CREATION DATE NOT NULL DEFAULT GETDATE()
)
GO

CREATE TABLE PRODUCT(
	IDPRODUCT INT PRIMARY KEY IDENTITY(500,1),
	PRODUCT_NAME VARCHAR(30) NOT NULL,
	PRODUCT_SIZE VARCHAR(20) NOT NULL,
	PRODUCT_COLOR VARCHAR(30) NOT NULL,
	PRODUCT_COMMENTS VARCHAR(255),
	PRODUCT_VALUE DECIMAL(10,2) NOT NULL,
	PRODUCT_CREATION DATE NOT NULL DEFAULT GETDATE(),
	ID_SUBCATEGORY INT NOT NULL DEFAULT 1, --FK SUBCATEGORY
	ID_BRAND INT NOT NULL DEFAULT 1--FK BRAND
)
GO

CREATE TABLE ITEM(
	IDITEM INT PRIMARY KEY IDENTITY(200,1),
	ITEM_QUANTITY INT NOT NULL DEFAULT 1,
	ID_PRODUCT INT NOT NULL DEFAULT 1, --FK PRODUCT
	ID_INVOICE INT NOT NULL DEFAULT 1, --FK INVOICE
)
GO

CREATE TABLE INVOICE(
	IDINVOICE INT PRIMARY KEY IDENTITY(1000,1),
	INVOICE_OPR CHAR(1) NOT NULL DEFAULT 'B', -- CHECK B OR S
	INVOICE_STATUS VARCHAR(10) NOT NULL DEFAULT 'ACTIVE',
	INVOICE_DATE DATE DEFAULT GETDATE(),
	ID_EMPLOYEE INT NOT NULL DEFAULT 1
)
GO

CREATE TABLE DEPARTMENT(
	IDDEPARTMENT INT PRIMARY KEY IDENTITY(1,1),
	DEPARTMENT_NAME VARCHAR(30) NOT NULL
)
GO

CREATE TABLE STORE(
	IDSTORE INT PRIMARY KEY IDENTITY(900,1),
	STORE_NAME VARCHAR(30) NOT NULL,
	STORE_ADDRESS VARCHAR(30) NOT NULL,
	STORE_CITY VARCHAR(30) NOT NULL,
	STORE_PROVINCE CHAR(2) NOT NULL DEFAULT 'ON'
)
GO

CREATE TABLE STORE_DEPARTMENT(
	IDSTORE_DEPARTMENT INT PRIMARY KEY IDENTITY,
	ID_STORE INT NOT NULL,
	ID_DEPARTMENT INT NOT NULL,
	ID_EMPLOYEE_MANAGER INT NOT NULL
)
GO

CREATE TABLE EMPLOYEE(
	IDEMPLOYEE INT PRIMARY KEY IDENTITY(300,1),
	EMPLOYEE_NAME VARCHAR(50) NOT NULL,
	EMPLOYEE_EMAIL VARCHAR(50) UNIQUE,
	EMPLOYEE_GENDER CHAR(1) NOT NULL,
	EMPLOYEE_ADMISSION DATE NOT NULL,
	EMPLOYEE_DEMISSION DATE,
	ID_STORE_DEPARTMENT INT, --FK STORE_DEPARTMENT
	ID_MANAGER INT DEFAULT NULL --FK EMPLOYEE	
)
GO

/****** Object:  FOREIGN KEYS CONSTRAINTS
******/

ALTER TABLE SUBCATEGORY ADD CONSTRAINT FK_SUBCATEGORY_CATEGORY
FOREIGN KEY(ID_CATEGORY) REFERENCES CATEGORY(IDCATEGORY)
GO

ALTER TABLE EMPLOYEE ADD CONSTRAINT FK_MANAGER
FOREIGN KEY(ID_MANAGER) REFERENCES EMPLOYEE(IDEMPLOYEE)
GO

ALTER TABLE STORE_DEPARTMENT ADD CONSTRAINT FK_STORE
FOREIGN KEY(ID_STORE) REFERENCES STORE(IDSTORE)
GO

ALTER TABLE STORE_DEPARTMENT ADD CONSTRAINT ID_DEPARTMENT
FOREIGN KEY(ID_DEPARTMENT) REFERENCES DEPARTMENT(IDDEPARTMENT)
GO

ALTER TABLE STORE_DEPARTMENT ADD CONSTRAINT FK_MANAGER_STORE
FOREIGN KEY(ID_EMPLOYEE_MANAGER) REFERENCES EMPLOYEE(IDEMPLOYEE)
GO

ALTER TABLE INVOICE ADD CONSTRAINT FK_INVOICE_EMPLOYEE
FOREIGN KEY(ID_EMPLOYEE) REFERENCES EMPLOYEE(IDEMPLOYEE)
GO

ALTER TABLE ITEM ADD CONSTRAINT FK_ITEM_PRODUCT
FOREIGN KEY(ID_PRODUCT) REFERENCES PRODUCT(IDPRODUCT)
GO

ALTER TABLE ITEM ADD CONSTRAINT FK_ITEM_INVOICE
FOREIGN KEY(ID_INVOICE) REFERENCES INVOICE(IDINVOICE)
GO

ALTER TABLE PRODUCT ADD CONSTRAINT FK_PRODUCT_SUBCATEGORY
FOREIGN KEY(ID_SUBCATEGORY) REFERENCES SUBCATEGORY(IDSUBCATEGORY)
GO

ALTER TABLE PRODUCT ADD CONSTRAINT FK_PRODUCT_BRAND
FOREIGN KEY(ID_BRAND) REFERENCES BRAND(IDBRAND)
GO
