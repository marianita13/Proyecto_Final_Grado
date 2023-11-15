CREATE DATABASE gardening CHARACTER SET utf8mb4;
USE gardening;

DROP TABLE IF EXISTS payment_detail;
DROP TABLE IF EXISTS method_payment;
DROP TABLE IF EXISTS order_detail;
DROP TABLE IF EXISTS product;
DROP TABLE IF EXISTS orders;
DROP TABLE IF EXISTS status;
DROP TABLE IF EXISTS client;
DROP TABLE IF EXISTS product_line;
DROP TABLE IF EXISTS employee;
DROP TABLE IF EXISTS office;
DROP TABLE IF EXISTS supplier;
DROP TABLE IF EXISTS person;
DROP TABLE IF EXISTS person_type;
DROP TABLE IF EXISTS postal_code;
DROP TABLE IF EXISTS city;
DROP TABLE IF EXISTS state;
DROP TABLE IF EXISTS country;


-- Tablas de Ubicación
CREATE TABLE country (
    country_id INT AUTO_INCREMENT PRIMARY KEY,
    country_name VARCHAR(50) NOT NULL
);

CREATE TABLE state (
    state_id INT AUTO_INCREMENT PRIMARY KEY,
    state_name VARCHAR(50) NOT NULL,
    country_id INT,
    FOREIGN KEY (country_id) REFERENCES country (country_id)
);

CREATE TABLE city (
    city_id INT AUTO_INCREMENT PRIMARY KEY,
    city_name VARCHAR(50) NOT NULL,
    state_id INT,
    FOREIGN KEY (state_id) REFERENCES state (state_id)
);

CREATE TABLE postal_code (
    postal_code_id INT AUTO_INCREMENT PRIMARY KEY,
    postal_code VARCHAR(10) NOT NULL,
    city_id INT,
    FOREIGN KEY (city_id) REFERENCES city (city_id)
);


CREATE TABLE person_type (
    type_id INT AUTO_INCREMENT PRIMARY KEY,
    type_name VARCHAR(50) NOT NULL
);

CREATE TABLE person (
    person_id INT AUTO_INCREMENT PRIMARY KEY,
    first_name VARCHAR(50) NOT NULL,
    last_name1 VARCHAR(50) NOT NULL,
    last_name2 VARCHAR(50) DEFAULT NULL,
    extension VARCHAR(10) NOT NULL,
    email VARCHAR(100) NOT NULL,
    person_type_id INT NOT NULL,
    postal_code_id INT,
    FOREIGN KEY (person_type_id) REFERENCES person_type (type_id),
    FOREIGN KEY (postal_code_id) REFERENCES postal_code (postal_code_id)
);

CREATE TABLE supplier (
    supplier_id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(30) NOT NULL,
    phone VARCHAR(15) NOT NULL,
    fax VARCHAR(15) NOT NULL,
    
);


CREATE TABLE office (
    office_code VARCHAR(10) NOT NULL,
    -- city_id INT NOT NULL,
    postal_code_id INT NOT NULL,
    phone VARCHAR(20) NOT NULL,
    address_line1 VARCHAR(50) NOT NULL,
    address_line2 VARCHAR(50) DEFAULT NULL,
    PRIMARY KEY (office_code),
    -- FOREIGN KEY (city_id) REFERENCES city (city_id),
    FOREIGN KEY (postal_code_id) REFERENCES postal_code (postal_code_id)
);

CREATE TABLE employee (
    employee_code INTEGER AUTO_INCREMENT NOT NULL,
    person_id INT NOT NULL,
    office_code VARCHAR(10) NOT NULL,
    manager_code INTEGER DEFAULT NULL,
    position VARCHAR(50) DEFAULT NULL,
    PRIMARY KEY (employee_code),
    FOREIGN KEY (person_id) REFERENCES person (person_id),
    FOREIGN KEY (office_code) REFERENCES office (office_code)
);

CREATE TABLE product_line (
    cod_product_line INTEGER AUTO_INCREMENT NOT NULL
    product_line VARCHAR(50) NOT NULL,
    description_text TEXT,
    description_html TEXT,
    image VARCHAR(256),
    PRIMARY KEY (cod_product_line)
);

CREATE TABLE client (
    client_code INTEGER AUTO_INCREMENT NOT NULL,
    person_id INT NOT NULL,
    phone VARCHAR(15) NOT NULL,
    fax VARCHAR(15) NOT NULL,
    credit_limit NUMERIC (15, 2) DEFAULT NULL,
    PRIMARY KEY (client_code),
    FOREIGN KEY (person_id) REFERENCES person (person_id)
);

CREATE TABLE status(
    cod_status INT AUTO_INCREMENT NOT NULL,
    name_status VARCHAR(20),
    PRIMARY KEY (cod_status)
);

CREATE TABLE orders (
    order_code INTEGER AUTO_INCREMENT NOT NULL,
    order_date DATE NOT NULL,
    expected_date DATE NOT NULL,
    delivery_date DATE DEFAULT NULL,
    status_code INT NOT NULL,
    comments TEXT,
    client_code INTEGER NOT NULL,
    PRIMARY KEY (order_code),
    FOREIGN KEY (status_code) REFERENCES status (cod_status),
    FOREIGN KEY (client_code) REFERENCES client (client_code)
);

CREATE TABLE product (
    product_code VARCHAR(15) NOT NULL,
    name VARCHAR(70) NOT NULL,
    product_line INT NOT NULL,
    dimensions VARCHAR(25) NULL,
    id_supplier INT NOT NULL,
    description TEXT NULL,
    stock_quantity SMALLINT NOT NULL,
    selling_price NUMERIC (15, 2) NOT NULL,
    supplier_price NUMERIC (15, 2) DEFAULT NULL,
    PRIMARY KEY (product_code),
    FOREIGN KEY (id_supplier) REFERENCES supplier (supplier_id),
    FOREIGN KEY (product_line) REFERENCES product_line (cod_product_line)
);

CREATE TABLE order_detail (
    order_code INTEGER NOT NULL,
    product_code VARCHAR (15) NOT NULL,
    quantity INTEGER NOT NULL,
    unit_price NUMERIC (15, 2) NOT NULL,
    line_number SMALLINT NOT NULL,
    PRIMARY KEY (order_code, product_code),
    FOREIGN KEY (order_code) REFERENCES orders (order_code),
    FOREIGN KEY (product_code) REFERENCES product (product_code)
);

CREATE TABLE method_payment(
    id_method INT AUTO_INCREMENT NOT NULL ,
    method_payment VARCHAR(20),
    PRIMARY KEY (id_method)
);

CREATE TABLE payment(
    client_code INT NOT NULL,
    method_id INT NOT NULL,
    transaction_id VARCHAR(50) NOT NULL,
    payment_date DATE NOT NULL,
    total NUMERIC (15, 2) NOT NULL,
    PRIMARY KEY (client_code, transaction_id),
    FOREIGN KEY (method_id) REFERENCES method_payment (id_method),
    FOREIGN KEY (client_code) REFERENCES client (client_code)
);

