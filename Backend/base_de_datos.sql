DROP DATABASE IF EXISTS gardening;
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

CREATE TABLE
    country (
        country_id INT AUTO_INCREMENT PRIMARY KEY,
        country_name VARCHAR(50) NOT NULL
    );

CREATE TABLE
    state (
        state_id INT AUTO_INCREMENT PRIMARY KEY,
        state_name VARCHAR(50) NOT NULL,
        country_id INT,
        FOREIGN KEY (country_id) REFERENCES country (country_id)
    );

CREATE TABLE
    city (
        city_id INT AUTO_INCREMENT PRIMARY KEY,
        city_name VARCHAR(50) NOT NULL,
        state_id INT,
        FOREIGN KEY (state_id) REFERENCES state (state_id)
    );

CREATE TABLE
    postal_code (
        postal_code_id INT AUTO_INCREMENT PRIMARY KEY,
        postal_code VARCHAR(10) NOT NULL,
        city_id INT,
        FOREIGN KEY (city_id) REFERENCES city (city_id)
    );

CREATE TABLE
    person_type (
        type_id INT AUTO_INCREMENT PRIMARY KEY,
        type_name VARCHAR(50) NOT NULL
    );

CREATE TABLE
    person (
        person_id INT AUTO_INCREMENT PRIMARY KEY,
        first_name VARCHAR(50) NOT NULL,
        last_name1 VARCHAR(50) NOT NULL,
        last_name2 VARCHAR(50) DEFAULT NULL,
        email VARCHAR(100) NOT NULL,
        person_type_id INT NOT NULL,
        FOREIGN KEY (person_type_id) REFERENCES person_type (type_id) 
    );

CREATE TABLE
    supplier (
        supplier_id INT AUTO_INCREMENT PRIMARY KEY,
        name VARCHAR(30) NOT NULL,
        phone VARCHAR(15) NOT NULL,
        fax VARCHAR(15) NOT NULL
    );

CREATE TABLE
    office (
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

CREATE TABLE
    employee (
        employee_code INTEGER AUTO_INCREMENT NOT NULL PRIMARY KEY,
        person_id INT NOT NULL,
        extention VARCHAR(10) NOT NULL,
        office_code VARCHAR(10) NOT NULL,
        manager_code INTEGER DEFAULT NULL,
        FOREIGN KEY (person_id) REFERENCES person (person_id),
        FOREIGN KEY (office_code) REFERENCES office (office_code)
    );

CREATE TABLE
    product_line (
        cod_product_line INTEGER AUTO_INCREMENT NOT NULL,
        product_line VARCHAR(50) NOT NULL,
        description_text TEXT,
        description_html TEXT,
        image VARCHAR(256),
        PRIMARY KEY (cod_product_line)
    );

CREATE TABLE
    client (
        client_code INTEGER AUTO_INCREMENT NOT NULL,
        client_name VARCHAR(50) NOT NULL,
        person_id INT NOT NULL,
        phone VARCHAR(15) NOT NULL,
        fax VARCHAR(15) NOT NULL,
        line_address VARCHAR(50) NOT NULL,
        line_address2 VARCHAR(50) DEFAULT NULL,
        postal_code_id INT NOT NULL,
        cod_employee INTEGER DEFAULT NULL,
        credit_limit NUMERIC (15, 2) DEFAULT NULL,
        PRIMARY KEY (client_code),
        FOREIGN KEY (person_id) REFERENCES person (person_id),
        FOREIGN KEY (postal_code_id) REFERENCES postal_code (postal_code_id),
        FOREIGN KEY (cod_employee) REFERENCES employee (employee_code)
    );

CREATE TABLE
    status(
        cod_status INT AUTO_INCREMENT NOT NULL,
        name_status VARCHAR(20),
        PRIMARY KEY (cod_status)
    );

CREATE TABLE
    orders (
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

CREATE TABLE
    product (
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

CREATE TABLE
    order_detail (
        order_code INTEGER NOT NULL,
        product_code VARCHAR (15) NOT NULL,
        quantity INTEGER NOT NULL,
        unit_price NUMERIC (15, 2) NOT NULL,
        line_number SMALLINT NOT NULL,
        PRIMARY KEY (order_code, product_code),
        FOREIGN KEY (order_code) REFERENCES orders (order_code),
        FOREIGN KEY (product_code) REFERENCES product (product_code)
    );

CREATE TABLE
    method_payment(
        id_method INT AUTO_INCREMENT NOT NULL,
        method_payment VARCHAR(20),
        PRIMARY KEY (id_method)
    );

CREATE TABLE
    payment(
        client_code INT NOT NULL,
        method_id INT NOT NULL,
        transaction_id VARCHAR(50) NOT NULL,
        payment_date DATE NOT NULL,
        total NUMERIC (15, 2) NOT NULL,
        PRIMARY KEY (client_code, transaction_id),
        FOREIGN KEY (method_id) REFERENCES method_payment (id_method),
        FOREIGN KEY (client_code) REFERENCES client (client_code)
    );

    
INSERT INTO country (country_name) VALUES 
('USA'),
('España'),
('Inglaterra'),
('Francia'),
('Australia'),
('Japón');

-- Inserciones en la tabla state
INSERT INTO state (state_name, country_id) VALUES 
('', 1), 
('', 4), 
('Barcelona', 2),
('MA', 2),
('EMEA', 3),
('EMEA', 4),
('Madrid', 4),   
('CA', 1), 
('APAC', 5), 
('Castilla-LaMancha', 2),
('Chiyoda-Ku', 6), 
('Cataluña', 2),
('Canarias', 2),
('Miami', 1),
('Islas Canarias', 2),
('Cadiz', 2),
('Nueva Gales del Sur', 5),
('London',3),
('Fuenlabrada', 2);

-- Inserciones en la tabla city
INSERT INTO city (city_name, state_id) VALUES 
('San Francisco', 8),
('Miami', 14),
('New York', 5),
('Fuenlabrada', 6),
('Madrid', 6),
('San Lorenzo del Escorial', 6),
('Montornes del valles', 3),
('Santa cruz de Tenerife', 15),
('Barcelona', 12),
('Canarias', 13),
('Sotogrande', 16),
('Humanes', 6),
('Getafe', 6),
('Boston', 4),
('Londres', 18),
('Paris', 2),
('Sydney', 17),
('Talavera de la Reina', 10),
('Tokyo', 11);

-- Inserciones en la tabla postal_code
INSERT INTO postal_code (postal_code, city_id) VALUES 
('24006', 1),
('24010', 2),
('85495', 3),
('696969', 2),
('28945', 4),
('28942', 5),
('28930', 5),
('28947', 5),
('28946', 5),
('28943', 5),
('28145', 6),
('28003', 5),
('28950', 5),
('24586', 7),
('28011', 5),
('38297', 8),
('12320', 9),
('35488', 10),
('11310', 11),
('28970', 12),
('28574', 4),
('29874', 5),
('37845', 5),
('28904', 13),
('28574', 12),
('08019', 9),
('02108', 14),
('EC2N1HN', 15),
('28032', 5),
('75017', 16),
('94080', 1),
('NSW 2010', 17),
('45632', 18),
('102-8578', 19),
('75010', 16),
('75058', 16),
('2000', 17),
('29643', 5),
('65930', 15),
('2003', 17);

-- inserciones tabla person_type
INSERT INTO person_type (type_id, type_name) VALUES
(1, 'Director General'),
(2, 'Subdirector Marketing'),
(3, 'Subdirector Ventas'),
(4, 'Secretaria'),
(5, 'Representante Ventas'),
(6, 'Director Oficina'),
(7, 'Cliente');

-- inserciones person
INSERT INTO person (first_name,last_name1,last_name2,email,person_type_id) VALUES 
('Marcos','Magaña','Perez','marcos@gardening.es',1),
('Ruben','López','Martinez','rlopez@gardening.es',2),
('Alberto','Soria','Carrasco','asoria@gardening.es',3),
('Maria','Solís','Jerez','msolis@gardening.es',4),
('Felipe','Rosas','Marquez','frosas@gardening.es',5),
('Juan Carlos','Ortiz','Serrano','cortiz@gardening.es',5),
('Carlos','Soria','Jimenez','csoria@gardening.es',6),
('Mariano','López','Murcia','mlopez@gardening.es',5),
('Lucio','Campoamor','Martín','lcampoamor@gardening.es',5),
('Hilario','Rodriguez','Huertas','hrodriguez@gardening.es',5),
('Emmanuel','Magaña','Perez','manu@gardening.es',6),
('José Manuel','Martinez','De la Osa','jmmart@hotmail.es',5),
('David','Palma','Aceituno','dpalma@gardening.es',5),
('Oscar','Palma','Aceituno','opalma@gardening.es',5),
('Francois','Fignon','','ffignon@gardening.com',6),
('Lionel','Narvaez','','lnarvaez@gardening.com',5),
('Laurent','Serra','','lserra@gardening.com',5),
('Michael','Bolton','','mbolton@gardening.com',6),
('Walter Santiago','Sanchez','Lopez','wssanchez@gardening.com',5),
('Hilary','Washington','','hwashington@gardening.com',6),
('Marcus','Paxton','','mpaxton@gardening.com',5),
('Lorena','Paxton','','lpaxton@gardening.com',5),
('Nei','Nishikori','','nnishikori@gardening.com',6),
('Narumi','Riko','','nriko@gardening.com',5),
('Takuma','Nomura','','tnomura@gardening.com',5),
('Amy','Johnson','','ajohnson@gardening.com',6),
('Larry','Westfalls','','lwestfalls@gardening.com',5),
('John','Walton','','jwalton@gardening.com',5),
('Kevin','Fallmer','','kfalmer@gardening.com',6),
('Julian','Bellinelli','','jbellinelli@gardening.com',5),
('Mariko','Kishi','','mkishi@gardening.com',5),
('Daniel G','GoldFish','','daniel.g@gardening.com',7),
('Anne','Wright','','anne.w@gardening.com',7),
('Link','Flaute','','link.f@gardening.com',7),
('Akane','Tendo','','akane.t@gardening.com',7),
('Antonio','Lasas','','antonio.l@gardening.com',7),
('Jose','Bermejo','','jose.b@gardening.com',7),
('Paco','Lopez','','paco.l@gardening.com',7),
('Naturagua','Guillermo','','guillermo.r@gardening.com',7),
('David','Serrano','','david.s@gardening.com',7),
('Jose','Tacaño','','jose.t@gardening.com',7),
('Antonio','Lasas','','antonio.l@gardening.com',7),
('Pedro','Camunas','','pedro.c@gardening.com',7),
('Juan','Rodriguez','','juan.r@gardening.com',7),
('Javier','Villar','','javier.v@gardening.com',7),
('Maria','Rodriguez','','maria.r@gardening.com',7),
('Beatriz','Fernandez','','beatriz.f@gardening.com',7),
('Victoria','Cruz','','victoria.c@gardening.com',7),
('Luis','Martinez','','luis.m@gardening.com',7),
('Mario','Suarez','','mario.s@gardening.com',7),
('Cristian','Rodrigez','','cristian.r@gardening.com',7),
('Francisco','Camacho','','francisco.c@gardening.com',7),
('Maria','Santillana','','maria.s@gardening.com',7),
('Federico','Gomez','','federico.g@gardening.com',7),
('Tony','Muñoz Mena','','tony.m@gardening.com',7),
('Eva María','Sánchez','','eva.s@gardening.com',7),
('Matías','San Martín','','matias.s@gardening.com',7),
('Benito','Lopez','','benito.l@gardening.com',7),
('Joseluis','Sanchez','','joseluis.s@gardening.com',7),
('Sara','Marquez','','sara.m@gardening.com',7),
('Luis','Jimenez','','luis.j@gardening.com',7),
('FraÃ§ois','Toulou','','francois.t@gardening.com',7),
('Pierre','Delacroux','','pierre.d@gardening.com',7),
('Jacob','Jones','','jacob.j@gardening.com',7),
('Antonio','Romero','','antonio.r@gardening.com',7),
('Richard','Mcain','','richard.m@gardening.com',7),
('Justin','Smith','','justin.s@gardening.com',7);


INSERT INTO office VALUES ('BCN-ES',26,'+34 93 3561182','Avenida Diagonal, 38','3A escalera Derecha'),
('BOS-USA',27,'+1 215 837 0825','1550 Court Place','Suite 102'),
('LON-UK',28,'+44 20 78772041','52 Old Broad Street','Ground Floor'),
('MAD-ES',29,'+34 91 7514487','Bulevar Indalecio Prieto, 32',''),
('PAR-FR',30,'+33 14 723 4404','29 Rue Jouffroy d''abbans',''),
('SFC-USA',31,'+1 650 219 4782','100 Market Street','Suite 300'),
('SYD-AU',32,'+61 2 9264 2451','5-11 Wentworth Avenue','Floor #2'),
('TAL-ES',33,'+34 925 867231','Francisco Aguirre, 32','5º piso (exterior)'),
('TOK-JP',34,'+81 33 224 5000','4-1 Kioicho','');

INSERT INTO employee(person_id,extention,office_code,manager_code) VALUES 
(1,'3897','TAL-ES',NULL),
(2,'2899','TAL-ES',1),
(3,'2837','TAL-ES',2),
(4,'2847','TAL-ES',2),
(5,'2844','TAL-ES',3),
(6,'2845','TAL-ES',3),
(7,'2444','MAD-ES',3),
(8,'2442','MAD-ES',7),
(9,'2442','MAD-ES',7),
(10,'2444','MAD-ES',7),
(11,'2518','BCN-ES',3),
(12,'2519','BCN-ES',11),
(13,'2519','BCN-ES',11),
(14,'2519','BCN-ES',11),
(15,'9981','PAR-FR',3),
(16,'9982','PAR-FR',15),
(17,'9982','PAR-FR',15),
(18,'7454','SFC-USA',3),
(19,'7454','SFC-USA',18),
(20,'7565','BOS-USA',3),
(21,'7565','BOS-USA',20),
(22,'7665','BOS-USA',20),
(23,'8734','TOK-JP',3),
(24,'8734','TOK-JP',23),
(25,'8735','TOK-JP',23),
(26,'3321','LON-UK',3),
(27,'3322','LON-UK',26),
(28,'3322','LON-UK',26),
(29,'3210','SYD-AU',3),
(30,'3211','SYD-AU',29),
(31,'3211','SYD-AU',29);

INSERT INTO client ( client_name, person_id, phone, fax, line_address, line_address2, postal_code_id, cod_employee, credit_limit) VALUES 
( 'GoldFish Garden', 32, '5556901745', '5556901746', 'False Street 52 2 A', NULL, 1, 19, 3000),
( 'Gardening Associates', 33, '5557410345', '5557410346', 'Wall-e Avenue', NULL, 2, 19, 6000),
( 'Gerudo Valley', 34, '5552323129', '5552323128', 'Oaks Avenue nº22', NULL, 3, 22, 12000),
( 'Tendo Garden', 35, '55591233210', '55591233211', 'Null Street nº69', NULL, 4, 22, 600000),
( 'Lasas S.A.', 36, '34916540145', '34914851312', 'C/Leganes 15', NULL, 5, 8, 154310),
( 'Beragua', 37, '654987321', '916549872', 'C/pintor segundo', 'Getafe', 6, 11, 20000),
( 'Club Golf Puerta del hierro', 38, '62456810', '919535678', 'C/sinesio delgado', 'Madrid', 7, 11, 40000),
( 'Naturagua', 39, '689234750', '916428956', 'C/majadahonda', 'Boadilla', 8, 11, 32000),
( 'DaraDistribuciones', 40, '675598001', '916421756', 'C/azores', 'Fuenlabrada', 9, 11, 50000),
( 'Madrileña de riegos', 41, '655983045', '916689215', 'C/Lagañas', 'Fuenlabrada', 10, 11, 20000),
( 'Lasas S.A.', 42, '34916540145', '34914851312', 'C/Leganes 15', NULL, 5, 8, 154310),
( 'Camunas Jardines S.L.', 43, '34914873241', '34914871541', 'C/Virgenes 45', 'C/Princesas 2 1ºB', 11, 8, 16481),
( 'Dardena S.A', 44, '34912453217', '34912484764', 'C/Nueva York 74', NULL, 12, 8, 321000),
( 'Jardin de Flores', 45, '654865643', '914538776', 'C/ Oña 34', NULL, 13, 30, 40000),
( 'Flores Marivi', 46, '666555444', '912458657', 'C/Leganes24', NULL, 5, 5, 1500),
( 'Flowers, S.A', 47, '698754159', '978453216', 'C/Luis Salquillo4', NULL, 14, 5, 3500),
( 'Naturajardin', 48, '612343529', '916548735', 'Plaza Magallón 15', NULL, 15, 30, 5050),
( 'Golf S.A.', 49, '916458762', '912354475', 'C/Estancado', NULL, 16, 12, 30000),
( 'Americh Golf Management SL', 50, '964493072', '964493063', 'C/Letardo', NULL, 17, 12, 20000),
( 'Aloha', 51, '916485852', '914489898', 'C/Roman 3', NULL, 18, 12, 50000),
( 'El Prat', 52, '916882323', '916493211', 'Avenida Tibidabo', NULL, 17, 12, 30000),
( 'Sotogrande', 53, '915576622', '914825645', 'C/Paseo del Parque', NULL, 19, 12, 60000),
( 'Vivero Humanes', 54, '654987690', '916040875', 'C/Miguel Echegaray 54', NULL, 20, 30, 7430),
( 'Fuenla City', 55, '675842139', '915483754', 'C/Callo 52', NULL, 21, 5, 4500),
( 'Jardines y Mansiones Cactus SL', 56, '916877445', '914477777', 'Polígono Industrial Maspalomas, Nº52', 'Móstoles', 26, 9, 76000),
( 'Jardinerías Matías SL', 57, '916544147', '917897474', 'C/Francisco Arce, Nº44', 'Bustarviejo', 23, 9, 100500),
( 'Agrojardin', 58, '675432926', '916549264', 'C/Mar Caspio 43', NULL, 24, 30, 8040),
( 'Top Campo', 59, '685746512', '974315924', 'C/Ibiza 32', NULL, 21, 5, 5500),
( 'Jardineria Sara', 60, '675124537', '912475843', 'C/Lima 1', NULL, 25, 5, 7500),
( 'Campohermoso', 61, '645925376', '916159116', 'C/Peru 78', NULL, 5, 30, 3250),
( 'france telecom', 62, '(33)5120578961', '(33)5120578961', '6 place d Alleray 15Ã¨me', NULL, 35, 16, 10000),
( 'Musée du Louvre', 63, '(33)0140205050', '(33)0140205442', 'Quai du Louvre', NULL, 36, 16, 30000),
( 'Tutifruti S.A', 64, '2 9261-2433', '2 9283-1695', 'level 24, St. Martins Tower.-31 Market St.', NULL, 37, 31, 10000),
( 'Flores S.L.', 65, '654352981', '685249700', 'Avenida España', NULL, 38, 18, 6000),
( 'The Magic Garden', 66, '926523468', '9364875882', 'Lihgting Park', NULL, 39, 18, 10000),
( 'El Jardin Viviente S.L', 67, '2 8005-7161', '2 8005-7162', '176 Cumberland Street The rocks', NULL, 40, 31, 8000);

INSERT INTO method_payment (method_payment) VALUES
 ('PayPal'),
 ('Transferencia'),
 ('Cheque');
 
INSERT INTO payment (client_code, method_id, transaction_id, payment_date, total) VALUES
 (1, 1, 'ak-std-000001', '2008-11-10', 2000),
 (1, 1, 'ak-std-000002', '2008-12-10', 2000),
 (2, 1, 'ak-std-000003', '2009-01-16', 5000),
 (2, 1, 'ak-std-000004', '2009-02-16', 5000),
 (2, 1, 'ak-std-000005', '2009-02-19', 926),
 (3, 1, 'ak-std-000006', '2007-01-08', 20000),
 (3, 1, 'ak-std-000007', '2007-01-08', 20000),
 (3, 1, 'ak-std-000008', '2007-01-08', 20000),
 (3, 1, 'ak-std-000009', '2007-01-08', 20000),
 (3, 1, 'ak-std-000010', '2007-01-08', 1849),
 (4, 2, 'ak-std-000011', '2006-01-18', 23794),
 (6, 3, 'ak-std-000012', '2009-01-13', 2390),
 (8, 1, 'ak-std-000013', '2009-01-06', 929),
 (12, 1, 'ak-std-000014', '2008-08-04', 2246),
 (13, 1, 'ak-std-000015', '2008-07-15', 4160),
 (14, 1, 'ak-std-000016', '2009-01-15', 2081),
 (16, 1, 'ak-std-000035', '2009-02-15', 10000),
 (17, 1, 'ak-std-000017', '2009-02-16', 4399),
 (18, 1, 'ak-std-000018', '2009-03-06', 232),
 (22, 1, 'ak-std-000019', '2009-03-26', 272),
 (25, 1, 'ak-std-000020', '2008-03-18', 18846),
 (26, 1, 'ak-std-000021', '2009-02-08', 10972),
 (27, 1, 'ak-std-000022', '2009-01-13', 8489),
 (29, 1, 'ak-std-000024', '2009-01-16', 7863),
 (33, 1, 'ak-std-000025', '2007-10-06', 3321),
 (36, 1, 'ak-std-000026', '2006-05-26', 1171);

 INSERT INTO status VALUES
 (1, 'Entregado'),
 (2, 'Rechazado'),
 (3, 'Pendiente');
 

INSERT INTO orders VALUES 
 (1,'2006-01-17','2006-01-19','2006-01-19',1,'Pagado a plazos',4),
 (2,'2007-10-23','2007-10-28','2007-10-26',1,'La entrega llego antes de lo esperado',4),
 (3,'2008-06-20','2008-06-25',NULL,2,'Limite de credito superado',4),
 (4,'2009-01-20','2009-01-26',NULL,3,NULL,4),
 (8,'2008-11-09','2008-11-14','2008-11-14',1,'El cliente paga la mitad con tarjeta y la otra mitad con efectivo, se le realizan dos facturas',1),
 (9,'2008-12-22','2008-12-27','2008-12-28',1,'El cliente comprueba la integridad del paquete, todo correcto',1),
 (10,'2009-01-15','2009-01-20',NULL,3,'El cliente llama para confirmar la fecha - Esperando al proveedor',2),
 (11,'2009-01-20','2009-01-27',NULL,3,'El cliente requiere que el pedido se le entregue de 16:00h a 22:00h',1),
 (12,'2009-01-22','2009-01-27',NULL,3,'El cliente requiere que el pedido se le entregue de 9:00h a 13:00h',1),
 (13,'2009-01-12','2009-01-14','2009-01-15',1,NULL,6),
 (14,'2009-01-02','2009-01-02',null,2,'mal pago',6),
 (15,'2009-01-09','2009-01-12','2009-01-11',1,NULL,6),
 (16,'2009-01-06','2009-01-07','2009-01-15',1,NULL,6),
 (17,'2009-01-08','2009-01-09','2009-01-11',1,'mal estado',6),
 (18,'2009-01-05','2009-01-06','2009-01-07',1,NULL,8),
 (19,'2009-01-18','2009-02-12',NULL,3,'entregar en murcia',8),
 (20,'2009-01-20','2009-02-15',NULL,3,NULL,8),
 (21,'2009-01-09','2009-01-09','2009-01-09',2,'mal pago',8),
 (22,'2009-01-11','2009-01-11','2009-01-13',1,NULL,8),
 (23,'2008-12-30','2009-01-10',NULL,2,'El pedido fue anulado por el cliente',4),
 (24,'2008-07-14','2008-07-31','2008-07-25',1,NULL,13),
 (25,'2009-02-02','2009-02-08',NULL,2,'El cliente carece de saldo en la cuenta asociada',1),
 (26,'2009-02-06','2009-02-12',NULL,2,'El cliente anula la operacion para adquirir mas producto',2),
 (27,'2009-02-07','2009-02-13',NULL,1,'El pedido aparece como entregado pero no sabemos en que fecha',2),
 (28,'2009-02-10','2009-02-17','2009-02-20',1,'El cliente se queja bastante de la espera asociada al producto',2),
 (29,'2008-08-01','2008-09-01','2008-09-01',2,'El cliente no está conforme con el pedido',13),
 (30,'2008-08-03','2008-09-03','2008-08-31',1,NULL,12),
 (31,'2008-09-04','2008-09-30','2008-10-04',2,'El cliente ha rechazado por llegar 5 dias tarde',12),
 (32,'2007-01-07','2007-01-19','2007-01-27',1,'Entrega tardia, el cliente puso reclamacion',3),
 (33,'2007-05-20','2007-05-28',NULL,2,'El pedido fue anulado por el cliente',3),
 (34,'2007-06-20','2008-06-28','2008-06-28',1,'Pagado a plazos',3),
 (35,'2008-03-10','2009-03-20',NULL,2,'Limite de credito superado',3),
 (36,'2008-10-15','2008-12-15','2008-12-10',1,NULL,13),
 (37,'2008-11-03','2009-11-13',NULL,3,'El pedido nunca llego a su destino',3),
 (38,'2009-03-05','2009-03-06','2009-03-07',1,NULL,18),
 (39,'2009-03-06','2009-03-07','2009-03-09',3,NULL,18),
 (40,'2009-03-09','2009-03-10','2009-03-13',2,NULL,18),
 (41,'2009-03-12','2009-03-13','2009-03-13',1,NULL,18),
 (42,'2009-03-22','2009-03-23','2009-03-27',1,NULL,18),
 (43,'2009-03-25','2009-03-26','2009-03-28',3,NULL,22),
 (44,'2009-03-26','2009-03-27','2009-03-30',3,NULL,22),
 (45,'2009-04-01','2009-03-04','2009-03-07',1,NULL,22),
 (46,'2009-04-03','2009-03-04','2009-03-05',2,NULL,22),
 (47,'2009-04-15','2009-03-17','2009-03-17',1,NULL,22),
 (48,'2008-03-17','2008-03-30','2008-03-29',1,'Según el Cliente, el pedido llegó defectuoso',25),
 (49,'2008-07-12','2008-07-22','2008-07-30',1,'El pedido llegó 1 día tarde, pero no hubo queja por parte de la empresa compradora',25),
 (50,'2008-03-17','2008-08-09',NULL,3,'Al parecer, el pedido se ha extraviado a la altura de Sotalbo (Ávila)',25),
 (51,'2008-10-01','2008-10-14','2008-10-14',1,'Todo se entregó a tiempo y en perfecto estado, a pesar del pésimo estado de las carreteras.',25),
 (52,'2008-12-07','2008-12-21',NULL,3,'El transportista ha llamado a Eva María para indicarle que el pedido llegará más tarde de lo esperado.',25),
 (53,'2008-10-15','2008-11-15','2008-11-09',1,'El pedido llega 6 dias antes',12),
 (54,'2009-01-11','2009-02-11',NULL,3,NULL,13),
 (55,'2008-12-10','2009-01-10','2009-01-11',1,'Retrasado 1 dia por problemas de transporte',13),
 (56,'2008-12-19','2009-01-20',NULL,2,'El cliente a anulado el pedido el dia 2009-01-10',12),
 (57,'2009-01-05','2009-02-05',NULL,3,NULL,12),
 (58,'2009-01-24','2009-01-31','2009-01-30',1,'Todo correcto',2),
 (59,'2008-11-09','2008-11-14','2008-11-14',1,'El cliente paga la mitad con tarjeta y la otra mitad con efectivo, se le realizan dos facturas',1),
 (60,'2008-12-22','2008-12-27','2008-12-28',1,'El cliente comprueba la integridad del paquete, todo correcto',1),
 (61,'2009-01-15','2009-01-20',NULL,3,'El cliente llama para confirmar la fecha - Esperando al proveedor',2),
 (62,'2009-01-20','2009-01-27',NULL,3,'El cliente requiere que el pedido se le entregue de 16:00h a 22:00h',1),
 (63,'2009-01-22','2009-01-27',NULL,3,'El cliente requiere que el pedido se le entregue de 9:00h a 13:00h',1),
 (64,'2009-01-24','2009-01-31','2009-01-30',1,'Todo correcto',1),
 (65,'2009-02-02','2009-02-08',NULL,2,'El cliente carece de saldo en la cuenta asociada',1),
 (66,'2009-02-06','2009-02-12',NULL,2,'El cliente anula la operacion para adquirir mas producto',2),
 (67,'2009-02-07','2009-02-13',NULL,1,'El pedido aparece como entregado pero no sabemos en que fecha',2),
 (68,'2009-02-10','2009-02-17','2009-02-20',1,'El cliente se queja bastante de la espera asociada al producto',2),
 (74,'2009-01-14','2009-01-22',NULL,2,'El pedido no llego el dia que queria el cliente por fallo del transporte',14),
 (75,'2009-01-11','2009-01-13','2009-01-13',1,'El pedido llego perfectamente',14),
 (76,'2008-11-15','2008-11-23','2008-11-23',1,NULL,14),
 (77,'2009-01-03','2009-01-08',NULL,3,'El pedido no pudo ser entregado por problemas meteorologicos',14),
 (78,'2008-12-15','2008-12-17','2008-12-17',1,'Fue entregado, pero faltaba mercancia que sera entregada otro dia',14),
 (79,'2009-01-12','2009-01-13','2009-01-13',1,NULL,27),
 (80,'2009-01-25','2009-01-26',NULL,3,'No terminó el pago',27),
 (81,'2009-01-18','2009-01-24',NULL,2,'Los producto estaban en mal estado',27),
 (82,'2009-01-20','2009-01-29','2009-01-29',1,'El pedido llego un poco mas tarde de la hora fijada',27),
 (83,'2009-01-24','2009-01-28',NULL,1,NULL,27),
 (89,'2007-10-05','2007-12-13','2007-12-10',1,'La entrega se realizo dias antes de la fecha esperada por lo que el cliente quedo satisfecho',33),
 (90,'2009-02-07','2008-02-17',NULL,3,'Debido a la nevada caída en la sierra, el pedido no podrá llegar hasta el día ',26),
 (91,'2009-03-18','2009-03-29','2009-03-27',1,'Todo se entregó a su debido tiempo, incluso con un día de antelación',26),
 (92,'2009-04-19','2009-04-30','2009-05-03',1,'El pedido se entregó tarde debido a la festividad celebrada en España durante esas fechas',26),
 (93,'2009-05-03','2009-05-30','2009-05-17',1,'El pedido se entregó antes de lo esperado.',26),
 (94,'2009-10-18','2009-11-01',NULL,3,'El pedido está en camino.',26),
 (95,'2008-01-04','2008-01-19','2008-01-19',1,NULL,33),
 (96,'2008-03-20','2008-04-12','2008-04-13',1,'La entrega se retraso undia',33),
 (97,'2008-10-08','2008-11-25','2008-11-25',1,NULL,33),
 (98,'2009-01-08','2009-02-13',NULL,3,NULL,33),
 (99,'2009-02-15','2009-02-27',NULL,3,NULL,15),
 (100,'2009-01-10','2009-01-15','2009-01-15',1,'El pedido llego perfectamente',15),
 (101,'2009-03-07','2009-03-27',NULL,2,'El pedido fue rechazado por el cliente',15),
 (102,'2008-12-28','2009-01-08','2009-01-08',1,'Pago pendiente',15),
 (103,'2009-01-15','2009-01-20','2009-01-24',3,NULL,29),
 (104,'2009-03-02','2009-03-06','2009-03-06',1,NULL,29),
 (105,'2009-02-14','2009-02-20',NULL,2,'el producto ha sido rechazado por la pesima calidad',29),
 (106,'2009-05-13','2009-05-15','2009-05-20',3,NULL,29),
 (107,'2009-04-06','2009-04-10','2009-04-10',1,NULL,29),
 (108,'2009-04-09','2009-04-15','2009-04-15',1,NULL,15),
 (109,'2006-05-25','2006-07-28','2006-07-28',1,NULL,36),
 (110,'2007-03-19','2007-04-24','2007-04-24',1,NULL,36),
 (111,'2008-03-05','2008-03-30','2008-03-30',1,NULL,34),
 (112,'2009-03-05','2009-04-06','2009-05-07',3,NULL,34),
 (113,'2008-10-28','2008-11-09','2009-01-09',2,'El producto ha sido rechazado por la tardanza de el envio',34),
 (114,'2009-01-15','2009-01-29','2009-01-31',1,'El envio llego dos dias más tarde debido al mal tiempo',34),
 (115,'2008-11-29','2009-01-26','2009-02-27',3,NULL,34),
 (116,'2008-06-28','2008-08-01','2008-08-01',1,NULL,36),
 (117,'2008-08-25','2008-10-01',NULL,2,'El pedido ha sido rechazado por la acumulacion de pago pendientes del cliente',36),
 (118,'2009-02-15','2009-02-27',NULL,3,NULL,15),
 (119,'2009-01-10','2009-01-15','2009-01-15',1,'El pedido llego perfectamente',15),
 (120,'2009-03-07','2009-03-27',NULL,2,'El pedido fue rechazado por elcliente',15),
 (121,'2008-12-28','2009-01-08','2009-01-08',1,'Pago pendiente',15),
 (122,'2009-04-09','2009-04-15','2009-04-15',1,NULL,15),
 (123,'2009-01-15','2009-01-20','2009-01-24',3,NULL,29),
 (124,'2009-03-02','2009-03-06','2009-03-06',1,NULL,29),
 (125,'2009-02-14','2009-02-20',NULL,2,'el producto ha sido rechazado por la pesima calidad',29),
 (126,'2009-05-13','2009-05-15','2009-05-20',3,NULL,29),
 (127,'2009-04-06','2009-04-10','2009-04-10',1,NULL,29),
 (128,'2008-11-10','2008-12-10','2008-12-29',2,'El pedido ha sido rechazado por el cliente por el retraso en la entrega',36);

INSERT INTO supplier VALUES
 (1, 'HiperGarden Tools', '+1234567890', 'Fax1234567890'),
 (2, 'Murcia Seasons', '+2345678901', 'Fax2345678901'),
 (3, 'Frutales Talavera S.A', '+3456789012', 'Fax3456789012'),
 (4, 'NaranjasValencianas.com', '+4567890123', 'Fax4567890123'),
 (5, 'Melocotones de Cieza S.A.', '+5678901234', 'Fax5678901234'),
 (6, 'Viveros EL OASIS', '+6789012345', 'Fax6789012345'),
 (7, 'Jerte Distribuciones S.L.','+57412441234','Fax5641241444'),
 (8,"Valencia Garden Service","+51441491241","Fax5908172347");


INSERT INTO product_line(product_line,description_text,description_html,image) VALUES 
('Herbaceas','Plantas para jardin decorativas',NULL,NULL),
('Herramientas','Herramientas para todo tipo de acción',NULL,NULL),
('Aromáticas','Plantas aromáticas',NULL,NULL),
('Frutales','Árboles pequeños de producción frutal',NULL,NULL),
('Ornamentales','Plantas vistosas para la decoración del jardín',NULL,NULL);


INSERT INTO product VALUES 
('11679','Sierra de Poda 400MM',2,'0,258',1,'Gracias a la poda se consigue
manipular un poco la naturaleza, dándole la forma que más nos guste. Este trabajo básico de jardinería
también facilita que las plantas crezcan de un modo más equilibrado, y que las flores y los frutos vuelvan
cada año con regularidad. Lo mejor es dar forma cuando los ejemplares son jóvenes, de modo que
exijan pocos cuidados cuando sean adultos. Además de saber cuándo y cómo hay que podar, tener unas
herramientas adecuadas para esta labor es también de vital importancia.',15,14,11),
 ('21636','Pala',2,'0,156',1,'Palas de acero con cresta de corte en la punta
para cortar bien el terreno. Buena penetración en tierras muy compactas.',15,14,13),
 ('22225','Rastrillo de Jardín',2,'1,064',1,'Fabuloso rastillo que le ayudará a
eliminar piedras, hojas, ramas y otros elementos incómodos en su jardín.',15,12,11),
 ('30310','Azadón',2,'0,168',1,'Longitud:24cm. Herramienta fabricada en
acero y pintura epoxi,alargando su durabilidad y preveniendo la corrosión.Diseño pensado para el
ahorro de trabajo.',15,12,11),
 ('AR-001','Ajedrea',3,'15-20',2,'Planta aromática que fresca se utiliza para
condimentar carnes y ensaladas, y seca, para pastas, sopas y guisantes',140,1,0),
 ('AR-002','Lavándula Dentata',3,'15-20',2,'Espliego de jardín, Alhucema
rizada, Alhucema dentada, Cantueso rizado. Familia: Lamiaceae.Origen: España y Portugal. Mata de
unos 60 cm de alto. Las hojas son aromáticas, dentadas y de color verde grisáceas. Produce compactas
espigas de flores pequeñas, ligeramente aromáticas, tubulares,de color azulado y con brácteas
púrpuras. Frutos: nuececillas alargadas encerradas en el tubo del cáliz. Se utiliza en jardineria y no
en perfumeria como otros cantuesos, espliegos y lavandas. Tiene propiedades aromatizantes y
calmantes. Adecuadas para la formación de setos bajos. Se dice que su aroma ahuyenta pulgones y
otros insectos perjudiciales para las plantas vecinas.',140,1,0),
 ('AR-003','Mejorana',3,'15-20',2,'Origanum majorana. No hay que
confundirlo con el orégano. Su sabor se parece más al tomillo, pero es más dulce y aromático.Se usan las
hojas frescas o secas, picadas, machacadas o en polvo, en sopas, rellenos, quiches y tartas, tortillas,
platos con papas y, como aderezo, en ramilletes de hierbas.El sabor delicado de la mejorana se elimina
durante la cocción, de manera que es mejor agregarla cuando el plato esté en su punto o en aquéllos
que apenas necesitan cocción.',140,1,0),
 ('AR-004','Melissa ',3,'15-20',2,'Es una planta perenne (dura varios años)
conocida por el agradable y característico olor a limón que desprenden en verano. Nunca debe faltar en
la huerta o jardín por su agradable aroma y por los variados usos que tiene: planta olorosa,
condimentaria y medicinal. Su cultivo es muy fácil. Le va bien un suelo ligero, con buen drenaje y riego
sin exceso. A pleno sol o por lo menos 5 horas de sol por día. Cada año, su abonado mineral
correspondiente.En otoño, la melisa pierde el agradable olor a limón que desprende en verano sus
flores azules y blancas. En este momento se debe cortar a unos 20 cm. del suelo. Brotará de forma
densa en primavera.',140,1,0),
 ('AR-005','Mentha Sativa',3,'15-20',2,'¿Quién no conoce la Hierbabuena?
Se trata de una plantita muy aromática, agradable y cultivada extensamente por toda España. Es hierba
perenne (por tanto vive varios años, no es anual). Puedes cultivarla en maceta o plantarla en la tierra del
jardín o en un rincón del huerto. Lo más importante es que cuente con bastante agua. En primavera
debes aportar fertilizantes minerales. Vive mejor en semisombra que a pleno sol.Si ves orugas o los
agujeros en hojas consecuencia de su ataque, retíralas una a una a mano; no uses insecticidas
químicos.',140,1,0),
 ('AR-006','Petrosilium Hortense (Peregil)',3,'15-20',2,'Nombre científico o
latino: Petroselinum hortense, Petroselinum crispum. Nombre común o vulgar: Perejil, Perejil rizado
Familia: Umbelliferae (Umbelíferas). Origen: el origen del perejil se encuentra en el Mediterraneo. Esta
naturalizada en casi toda Europa. Se utiliza como condimento y para adorno, pero también en
ensaladas. Se suele regalar en las fruterías y verdulerías.El perejil lo hay de 2 tipos: de hojas planas y de
hojas rizadas.',140,1,0),
 ('AR-007','Salvia Mix',3,'15-20',2,'La Salvia es un pequeño arbusto que
llega hasta el metro de alto.Tiene una vida breve, de unos pocos años.En el jardín, como otras
aromáticas, queda muy bien en una rocalla o para hacer una bordura perfumada a cada lado de un
camino de Salvia. Abona después de cada corte y recorta el arbusto una vez pase la floración.',140,1,0),
 ('AR-008','Thymus Citriodra (Tomillo limón)',3,'15-20',2,'Nombre común o
vulgar: Tomillo, Tremoncillo Familia: Labiatae (Labiadas).Origen: Región mediterránea.Arbustillo bajo, de
15 a 40 cm de altura. Las hojas son muy pequeñas, de unos 6 mm de longitud; según la variedad pueden
ser verdes, verdes grisáceas, amarillas, o jaspeadas. Las flores aparecen de mediados de primavera hasta
bien entrada la época estival y se presentan en racimos terminales que habitualmente son de color
violeta o púrpura aunque también pueden ser blancas. Esta planta despide un intenso y típico aroma,
que se incrementa con el roce. El tomillo resulta de gran belleza cuando está en flor. El tomillo atrae a
avispas y abejas. En jardinería se usa como manchas, para hacer borduras, para aromatizar el ambiente,
llenar huecos, cubrir rocas, para jardines en miniatura, etc. Arranque las flores y hojas secas del tallo y
añadálos a un popurri, introdúzcalos en saquitos de hierbas o en la almohada.También puede usar las
ramas secas con flores para añadir aroma y textura a cestos abiertos.',140,1,0),
 ('AR-009','Thymus Vulgaris',3,'15-20',2,'Nombre común o vulgar: Tomillo,
Tremoncillo Familia: Labiatae (Labiadas). Origen: Región mediterránea. Arbustillo bajo, de 15 a 40 cm de
altura. Las hojas son muy pequeñas, de unos 6 mm de longitud; según la variedad pueden ser verdes,
verdes grisáceas, amarillas, o jaspeadas. Las flores aparecen de mediados de primavera hasta bien
entrada la época estival y se presentan en racimos terminales que habitualmente son de color violeta o
púrpura aunque también pueden ser blancas. Esta planta despide un intenso y típico aroma, que se
incrementa con el roce. El tomillo resulta de gran belleza cuando está en flor. El tomillo atrae a avispas y
abejas.\r\n En jardinería se usa como manchas, para hacer borduras, para aromatizar el ambiente, llenar
huecos, cubrir rocas, para jardines en miniatura, etc. Arranque las flores y hojas secas del tallo y
añadálos a un popurri, introdúzcalos en saquitos de hierbas o en la almohada. También puede usar las
ramas secas con flores para añadir aroma y textura a cestos abiertos.',140,1,0),
 ('AR-010','Santolina Chamaecyparys',3,'15-20',2,'',140,1,0),
 ('FR-1','Expositor Cítricos Mix',4,'100-120',3,'',15,7,5),
 ('FR-10','Limonero 2 años injerto',4,'',4,'El limonero, pertenece al grupo de
los cítricos, teniendo su origen hace unos 20 millones de años en el sudeste asiático. Fue introducido por
los árabes en el área mediterránea entre los años 1.000 a 1.200, habiendo experimentando numerosas
modificaciones debidas tanto a la selección natural mediante hibridaciones espontáneas como a las
producidas por el hombre, en este caso buscando las necesidades del mercado.',15,7,5),
 ('FR-100','Nectarina',4,'8/10',3,'Se trata de un árbol derivado por mutación
de los melocotoneros comunes, y los únicos caracteres diferenciales son la ausencia de tomentosidad en
la piel del fruto. La planta, si se deja crecer libremente, adopta un porte globoso con unas dimensiones
medias de 4-6 metros',50,11,8),
 ('FR-101','Nogal',4,'8/10',3,'',50,13,10),
 ('FR-102','Olea-Olivos',4,'8/10',3,'Existen dos hipótesis sobre el origen del
olivo, una que postula que proviene de las costas de Siria, Líbano e Israel y otra que considera que lo
considera originario de Asia menor. La llegada a Europa probablemente tuvo lugar de mano de los
Fenicios, en transito por Chipre, Creta, e Islas del Mar Egeo, pasando a Grecia y más tarde a Italia. Los
primeros indicios de la presencia del olivo en las costas mediterráneas españolas coinciden con el
dominio romano, aunque fueron posteriormente los árabes los que impulsaron su cultivo en Andalucía,
convirtiendo a España en el primer país productr de aceite de oliva a nivel mundial.',50,18,14),
 ('FR-103','Olea-Olivos',4,'10/12',3,'Existen dos hipótesis sobre el origen del
olivo, una que postula que proviene de las costas de Siria, Líbano e Israel y otra que considera que lo
considera originario de Asia menor. La llegada a Europa probablemente tuvo lugar de mano de los
Fenicios, en transito por Chipre, Creta, e Islas del Mar Egeo, pasando a Grecia y más tarde a Italia. Los
primeros indicios de la presencia del olivo en las costas mediterráneas españolas coinciden con el
dominio romano, aunque fueron posteriormente los árabes los que impulsaron su cultivo en Andalucía,
convirtiendo a España en el primer país productr de aceite de oliva a nivel mundial.',50,25,20),
 ('FR-104','Olea-Olivos',4,'12/4',3,'Existen dos hipótesis sobre el origen del
olivo, una que postula que proviene de las costas de Siria, Líbano e Israel y otra que considera que lo
considera originario de Asia menor. La llegada a Europa probablemente tuvo lugar de mano de los
Fenicios, en transito por Chipre, Creta, e Islas del Mar Egeo, pasando a Grecia y más tarde a Italia. Los
primeros indicios de la presencia del olivo en las costas mediterráneas españolas coinciden con el
dominio romano, aunque fueron posteriormente los árabes los que impulsaron su cultivo en Andalucía,
convirtiendo a España en el primer país productr de aceite de oliva a nivel mundial.',50,49,39),
 ('FR-105','Olea-Olivos',4,'14/16',3,'Existen dos hipótesis sobre el origen del
olivo, una que postula que proviene de las costas de Siria, Líbano e Israel y otra que considera que lo
considera originario de Asia menor. La llegada a Europa probablemente tuvo lugar de mano de los
Fenicios, en transito por Chipre, Creta, e Islas del Mar Egeo, pasando a Grecia y más tarde a Italia. Los
primeros indicios de la presencia del olivo en las costas mediterráneas españolas coinciden con el
dominio romano, aunque fueron posteriormente los árabes los que impulsaron su cultivo en Andalucía,
convirtiendo a España en el primer país productr de aceite de oliva a nivel mundial.',50,70,56),
 ('FR-106','Peral',4,'8/10',3,'Árbol piramidal, redondeado en su juventud,
luego oval, que llega hasta 20 metros de altura y por término medio vive 65 años.Tronco alto, grueso, de
corteza agrietada, gris, de la cual se destacan con frecuencia placas lenticulares.Las ramas se insertan
formando ángulo agudo con el tronco (45º), de corteza lisa, primero verde y luego gris-violácea, con
numerosas lenticelas.',50,11,8),
 ('FR-107','Peral',4,'10/12',3,'Árbol piramidal, redondeado en su juventud,
luego oval, que llega hasta 20 metros de altura y por término medio vive 65 años.Tronco alto, grueso, de
corteza agrietada, gris, de la cual se destacan con frecuencia placas lenticulares.Las ramas se insertan
formando ángulo agudo con el tronco (45º), de corteza lisa, primero verde y luego gris-violácea, con
numerosas lenticelas.',50,22,17),
 ('FR-108','Peral',4,'12/14',3,'Árbol piramidal, redondeado en su juventud,
luego oval, que llega hasta 20 metros de altura y por término medio vive 65 años.Tronco alto, grueso, de
corteza agrietada, gris, de la cual se destacan con frecuencia placas lenticulares.Las ramas se insertan
formando ángulo agudo con el tronco (45º), de corteza lisa, primero verde y luego gris-violácea, con
numerosas lenticelas.',50,32,25),
 ('FR-11','Limonero 30/40',4,'',4,'El limonero, pertenece al grupo de los
cítricos, teniendo su origen hace unos 20 millones de años en el sudeste asiático. Fue introducido por los
árabes en el área mediterránea entre los años 1.000 a 1.200, habiendo experimentando numerosas
modificaciones debidas tanto a la selección natural mediante hibridaciones espontáneas como a las
producidas por el',15,100,80),
 ('FR-12','Kunquat ',4,'',4,'su nombre científico se origina en honor a un
hoticultor escocés que recolectó especímenes en China, (\"Fortunella\"), Robert Fortune (1812-1880), y
\"margarita\", del latín margaritus-a-um = perla, en alusión a sus pequeños y brillantes frutos. Se trata
de un arbusto o árbol pequeño de 2-3 m de altura, inerme o con escasas espinas.Hojas lanceoladas de
4-8 (-15) cm de longitud, con el ápice redondeado y la base cuneada.Tienen el margen crenulado en su
mitad superior, el haz verde brillante y el envés más pálido.Pecíolo ligeramente marginado.Flores
perfumadas solitarias o agrupadas en inflorescencias axilares, blancas.El fruto es lo más característico,
es el más pequeño de todos los cítricos y el único cuya cáscara se puede comer.Frutos pequeños, con
semillas, de corteza fina, dulce, aromática y comestible, y de pulpa naranja amarillenta y ligeramente
ácida.Sus frutos son muy pequeños y tienen un carácter principalmente ornamental.',15,21,16),
 ('FR-13','Kunquat EXTRA con FRUTA',4,'150-170',4,'su nombre científico
se origina en honor a un hoticultor escocés que recolectó especímenes en China, (\"Fortunella\"),
Robert Fortune (1812-1880), y \"margarita\", del latín margaritus-a-um = perla, en alusión a sus
pequeños y brillantes frutos. Se trata de un arbusto o árbol pequeño de 2-3 m de altura, inerme o con
escasas espinas.Hojas lanceoladas de 4-8 (-15) cm de longitud, con el ápice redondeado y la base
cuneada.Tienen el margen crenulado en su mitad superior, el haz verde brillante y el envés más
pálido.Pecíolo ligeramente marginado.Flores perfumadas solitarias o agrupadas en inflorescencias
axilares, blancas.El fruto es lo más característico, es el más pequeño de todos los cítricos y el único cuya
cáscara se puede comer.Frutos pequeños, con semillas, de corteza fina, dulce, aromática y comestible, y
de pulpa naranja amarillenta y ligeramente ácida.Sus frutos son muy pequeños y tienen un carácter
principalmente ornamental.',15,57,45),
 ('FR-14','Calamondin Mini',4,'',3,'Se trata de un pequeño arbolito de copa
densa, con tendencia a la verticalidad, inerme o con cortas espinas. Sus hojas son pequeñas, elípticas de
5-10 cm de longitud, con los pecíolos estrechamente alados.Posee 1 o 2 flores en situación axilar, al final
de las ramillas.Sus frutos son muy pequeños (3-3,5 cm de diámetro), con pocas semillas, esféricos u
ovales, con la zona apical aplanada; corteza de color naranja-rojizo, muy fina y fácilmente separable de
la pulpa, que es dulce, ácida y comestible..',15,10,8),
 ('FR-15','Calamondin Copa ',4,'',3,'Se trata de un pequeño arbolito de copa
densa, con tendencia a la verticalidad, inerme o con cortas espinas. Sus hojas son pequeñas, elípticas de
5-10 cm de longitud, con los pecíolos estrechamente alados.Posee 1 o 2 flores en situación axilar, al final
de las ramillas.Sus frutos son muy pequeños (3-3,5 cm de diámetro), con pocas semillas, esféricos u
ovales, con la zona apical aplanada; corteza de color naranja-rojizo, muy fina y fácilmente separable de
la pulpa, que es dulce, ácida y comestible..',15,25,20),
 ('FR-16','Calamondin Copa EXTRA Con FRUTA',4,'100-120',3,'Se trata de un
pequeño arbolito de copa densa, con tendencia a la verticalidad, inerme o con cortas espinas. Sus hojas
son pequeñas, elípticas de 5-10 cm de longitud, con los pecíolos estrechamente alados.Posee 1 o 2
flores en situación axilar, al final de las ramillas.Sus frutos son muy pequeños (3-3,5 cm de diámetro),
con pocas semillas, esféricos u ovales, con la zona apical aplanada; corteza de color naranja-rojizo, muy
fina y fácilmente separable de la pulpa, que es dulce, ácida y comestible..',15,45,36),
 ('FR-17','Rosal bajo 1Âª -En maceta-inicio brotación',4,'',3,'',15,2,1),
 ('FR-18','ROSAL TREPADOR',4,'',3,'',350,4,3),
 ('FR-19','Camelia Blanco, Chrysler Rojo, Soraya Naranja, ',4,'',4,'',350,4,3),
 ('FR-2','Naranjo -Plantón joven 1 año injerto',4,'',4,'El naranjo es un árbol
pequeño, que no supera los 3-5 metros de altura, con una copa compacta, cónica, transformada en
esérica gracias a la poda. Su tronco es de color gris y liso, y las hojas son perennes, coriáceas, de un
verde intenso y brillante, con forma oval o elíptico-lanceolada. Poseen, en el caso del naranjo amargo,
un típico peciolo alado en forma de Â‘corazónÂ’, que en el naranjo dulce es más estrecho y menos
patente.',15,6,4),
 ('FR-20','Landora Amarillo, Rose Gaujard bicolor
blanco-rojo',4,'',3,'',350,4,3),
 ('FR-21','Kordes Perfect bicolor rojo-amarillo, Roundelay rojo
fuerte',4,'',3,'',350,4,3),
 ('FR-22','Pitimini rojo',4,'',3,'',350,4,3),
 ('FR-23','Rosal copa ',4,'',3,'',400,8,6), 
 ('FR-24','Albaricoquero Corbato',4,'',5,'árbol que puede pasar de los 6 m
de altura, en la región mediterránea con ramas formando una copa redondeada. La corteza del tronco
es pardo-violácea, agrietada; las ramas son rojizas y extendidas cuando jóvenes y las ramas secundarias
son cortas, divergentes y escasas. Las yemas latentes son frecuentes especialmente sobre las ramas
viejas.',400,8,6),
 ('FR-25','Albaricoquero Moniqui',4,'',5,'árbol que puede pasar de los 6 m
de altura, en la región mediterránea con ramas formando una copa redondeada. La corteza del tronco
es pardo-violácea, agrietada; las ramas son rojizas y extendidas cuando jóvenes y las ramas secundarias
son cortas, divergentes y escasas. Las yemas latentes son frecuentes especialmente sobre las ramas
viejas.',400,8,6),
 ('FR-26','Albaricoquero Kurrot',4,'',5,'árbol que puede pasar de los 6 m de
altura, en la región mediterránea con ramas formando una copa redondeada. La corteza del tronco es
pardo-violácea, agrietada; las ramas son rojizas y extendidas cuando jóvenes y las ramas secundarias son
cortas, divergentes y escasas. Las yemas latentes son frecuentes especialmente sobre las ramas
viejas.',400,8,6),
 ('FR-27','Cerezo Burlat',4,'',7,'Las principales
especies de cerezo cultivadas en el mundo son el cerezo dulce (Prunus avium), el guindo (P. cerasus) y el
cerezo \"Duke\", híbrido de los anteriores. Ambas especies son naturales del sureste de Europa y oeste
de Asia. El cerezo dulce tuvo su origen probablemente en el mar Negro y en el mar Caspio,
difundiéndose después hacia Europa y Asia, llevado por los pájaros y las migraciones humanas. Fue uno
de los frutales más apreciados por los griegos y con el Imperio Romano se extendió a regiones muy
diversas. En la actualidad, el cerezo se encuentra difundido por numerosas regiones y países del mundo
con clima templado',400,8,6),
 ('FR-28','Cerezo Picota',4,'',7,'Las principales
especies de cerezo cultivadas en el mundo son el cerezo dulce (Prunus avium), el guindo (P. cerasus) y el
cerezo \"Duke\", híbrido de los anteriores. Ambas especies son naturales del sureste de Europa y oeste
de Asia. El cerezo dulce tuvo su origen probablemente en el mar Negro y en el mar Caspio,
difundiéndose después hacia Europa y Asia, llevado por los pájaros y las migraciones humanas. Fue uno
de los frutales más apreciados por los griegos y con el Imperio Romano se extendió a regiones muy
diversas. En la actualidad, el cerezo se encuentra difundido por numerosas regiones y países del mundo
con clima templado',400,8,6),
 ('FR-29','Cerezo Napoleón',4,'',7,'Las principales
especies de cerezo cultivadas en el mundo son el cerezo dulce (Prunus avium), el guindo (P. cerasus) y el
cerezo \"Duke\", híbrido de los anteriores. Ambas especies son naturales del sureste de Europa y oeste
de Asia. El cerezo dulce tuvo su origen probablemente en el mar Negro y en el mar Caspio,
difundiéndose después hacia Europa y Asia, llevado por los pájaros y las migraciones humanas. Fue uno
de los frutales más apreciados por los griegos y con el Imperio Romano se extendió a regiones muy
diversas. En la actualidad, el cerezo se encuentra difundido por numerosas regiones y países del mundo
con clima templado',400,8,6),
 ('FR-3','Naranjo 2 años injerto',4,'',4,'El naranjo es un árbol pequeño, que
no supera los 3-5 metros de altura, con una copa compacta, cónica, transformada en esérica gracias a la
poda. Su tronco es de color gris y liso, y las hojas son perennes, coriáceas, de un verde intenso y
brillante, con forma oval o elíptico-lanceolada. Poseen, en el caso del naranjo amargo, un típico peciolo
alado en forma de Â‘corazónÂ’, que en el naranjo dulce es más estrecho y menos patente.',15,7,5),
 ('FR-30','Ciruelo R. Claudia Verde ',4,'',3,'árbol de tamaño mediano que
alcanza una altura máxima de 5-6 m. Tronco de corteza pardo-azulada, brillante, lisa o agrietada
longitudinalmente. Produce ramas alternas, pequeñas, delgadas, unas veces lisas, glabras y otras
pubescentes y vellosas',400,8,6),
 ('FR-31','Ciruelo Santa Rosa',4,'',3,'árbol de tamaño mediano que alcanza
una altura máxima de 5-6 m. Tronco de corteza pardo-azulada, brillante, lisa o agrietada
longitudinalmente. Produce ramas alternas, pequeñas, delgadas, unas veces lisas, glabras y otras
pubescentes y vellosas',400,8,6),
 ('FR-32','Ciruelo Golden Japan',4,'',3,'árbol de tamaño mediano que
alcanza una altura máxima de 5-6 m. Tronco de corteza pardo-azulada, brillante, lisa o agrietada
longitudinalmente. Produce ramas alternas, pequeñas, delgadas, unas veces lisas, glabras y otras
pubescentes y vellosas',400,8,6),
 ('FR-33','Ciruelo Friar',4,'',3,'árbol de tamaño mediano que alcanza una
altura máxima de 5-6 m. Tronco de corteza pardo-azulada, brillante, lisa o agrietada longitudinalmente.
Produce ramas alternas, pequeñas, delgadas, unas veces lisas, glabras y otras pubescentes y
vellosas',400,8,6),
 ('FR-34','Ciruelo Reina C. De Ollins',4,'',3,'árbol de tamaño mediano que
alcanza una altura máxima de 5-6 m. Tronco de corteza pardo-azulada, brillante, lisa o agrietada
longitudinalmente. Produce ramas alternas, pequeñas, delgadas, unas veces lisas, glabras y otras
pubescentes y vellosas',400,8,6),
 ('FR-35','Ciruelo Claudia Negra',4,'',3,'árbol de tamaño mediano que
alcanza una altura máxima de 5-6 m. Tronco de corteza pardo-azulada, brillante, lisa o agrietada
longitudinalmente. Produce ramas alternas, pequeñas, delgadas, unas veces lisas, glabras y otras
pubescentes y vellosas',400,8,6),
 ('FR-36','Granado Mollar de Elche',4,'',3,'pequeño árbol caducifolio, a
veces con porte arbustivo, de 3 a 6 m de altura, con el tronco retorcido. Madera dura y corteza
escamosa de color grisáceo. Las ramitas jóvenes son más o menos cuadrangulares o angostas y de
cuatro alas, posteriormente se vuelven redondas con corteza de color café grisáceo, la mayoría de las
ramas, pero especialmente las pequeñas ramitas axilares, son en forma de espina o terminan en una
espina aguda; la copa es extendida.',400,9,7),
 ('FR-37','Higuera Napolitana',4,'',3,'La higuera (Ficus carica L.) es un árbol
típico de secano en los países mediterráneos. Su rusticidad y su fácil multiplicación hacen de la higuera
un frutal muy apropiado para el cultivo extensivo.. Siempre ha sido considerado como árbol que no
requiere cuidado alguno una vez plantado y arraigado, limitándose el hombre a recoger de él los frutos
cuando maduran, unos para consumo en fresco y otros para conserva. Las únicas higueras con cuidados
culturales esmerados, en muchas comarcas, son las brevales, por el interés económico de su primera
cosecha, la de brevas.',400,9,7),
 ('FR-38','Higuera Verdal',4,'',3,'La higuera (Ficus carica L.) es un árbol típico
de secano en los países mediterráneos. Su rusticidad y su fácil multiplicación hacen de la higuera un
frutal muy apropiado para el cultivo extensivo.. Siempre ha sido considerado como árbol que no
requiere cuidado alguno una vez plantado y arraigado, limitándose el hombre a recoger de él los frutos
cuando maduran, unos para consumo en fresco y otros para conserva. Las únicas higueras con cuidados
culturales esmerados, en muchas comarcas, son las brevales, por el interés económico de su primera
cosecha, la de brevas.',400,9,7),
 ('FR-39','Higuera Breva',4,'',3,'La higuera (Ficus carica L.) es un árbol típico
de secano en los países mediterráneos. Su rusticidad y su fácil multiplicación hacen de la higuera un
frutal muy apropiado para el cultivo extensivo.. Siempre ha sido considerado como árbol que no
requiere cuidado alguno una vez plantado y arraigado, limitándose el hombre a recoger de él los frutos
cuando maduran, unos para consumo en fresco y otros para conserva. Las únicas higueras con cuidados
culturales esmerados, en muchas comarcas, son las brevales, por el interés económico de su primera
cosecha, la de brevas.',400,9,7),
 ('FR-4','Naranjo calibre 8/10',4,'',4,'El naranjo es un árbol pequeño, que no
supera los 3-5 metros de altura, con una copa compacta, cónica, transformada en esérica gracias a la
poda. Su tronco es de color gris y liso, y las hojas son perennes, coriáceas, de un verde intenso y
brillante, con forma oval o elíptico-lanceolada. Poseen, en el caso del naranjo amargo, un típico peciolo
alado en forma de Â‘corazónÂ’, que en el naranjo dulce es más estrecho y menos patente.',15,29,23),
 ('FR-40','Manzano Starking Delicious',4,'',3,'alcanza como máximo 10 m.
de altura y tiene una copa globosa. Tronco derecho que normalmente alcanza de 2 a 2,5 m. de altura,
con corteza cubierta de lenticelas, lisa, adherida, de color ceniciento verdoso sobre los ramos y
escamosa y gris parda sobre las partes viejas del árbol. Tiene una vida de unos 60-80 años. Las ramas se
insertan en ángulo abierto sobre el tallo, de color verde oscuro, a veces tendiendo a negruzco o
violáceo. Los brotes jóvenes terminan con frecuencia en una espina',400,8,6),
 ('FR-41','Manzano Reineta',4,'',3,'alcanza como máximo 10 m. de altura y
tiene una copa globosa. Tronco derecho que normalmente alcanza de 2 a 2,5 m. de altura, con corteza
cubierta de lenticelas, lisa, adherida, de color ceniciento verdoso sobre los ramos y escamosa y gris
parda sobre las partes viejas del árbol. Tiene una vida de unos 60-80 años. Las ramas se insertan en
ángulo abierto sobre el tallo, de color verde oscuro, a veces tendiendo a negruzco o violáceo. Los brotes
jóvenes terminan con frecuencia en una espina',400,8,6),
 ('FR-42','Manzano Golden Delicious',4,'',3,'alcanza como máximo 10 m. de
altura y tiene una copa globosa. Tronco derecho que normalmente alcanza de 2 a 2,5 m. de altura, con
corteza cubierta de lenticelas, lisa, adherida, de color ceniciento verdoso sobre los ramos y escamosa y
gris parda sobre las partes viejas del árbol. Tiene una vida de unos 60-80 años. Las ramas se insertan en
ángulo abierto sobre el tallo, de color verde oscuro, a veces tendiendo a negruzco o violáceo. Los brotes
jóvenes terminan con frecuencia en una espina',400,8,6),
 ('FR-43','Membrillero Gigante de Wranja',4,'',3,'',400,8,6),
 ('FR-44','Melocotonero Spring Crest',4,'',5,'Árbol caducifolio de porte bajo
con corteza lisa, de color ceniciento. Sus hojas son alargadas con el margen ligeramente aserrado, de
color verde brillante, algo más claras por el envés. El melocotonero está muy arraigado en la cultura
asiática.\r\nEn Japón, el noble heroe Momotaro, una especie de Cid japonés, nació del interior de un
enorme melocotón que flotaba río abajo.\r\nEn China se piensa que comer melocotón confiere
longevidad al ser humano, ya que formaba parte de la dieta de sus dioses inmortales.',400,8,6),
 ('FR-45','Melocotonero Amarillo de Agosto',4,'',5,'Árbol caducifolio de
porte bajo con corteza lisa, de color ceniciento. Sus hojas son alargadas con el margen ligeramente
aserrado, de color verde brillante, algo más claras por el envés. El melocotonero está muy arraigado en
la cultura asiática.\r\nEn Japón, el noble heroe Momotaro, una especie de Cid japonés, nació del interior
de un enorme melocotón que flotaba río abajo.\r\nEn China se piensa que comer melocotón confiere
longevidad al ser humano, ya que formaba parte de la dieta de sus dioses inmortales.',400,8,6),
 ('FR-46','Melocotonero Federica',4,'',5,'Árbol caducifolio de porte bajo con
corteza lisa, de color ceniciento. Sus hojas son alargadas con el margen ligeramente aserrado, de color
verde brillante, algo más claras por el envés. El melocotonero está muy arraigado en la cultura
asiática.\r\nEn Japón, el noble heroe Momotaro, una especie de Cid japonés, nació del interior de un
enorme melocotón que flotaba río abajo.\r\nEn China se piensa que comer melocotón confiere
longevidad al ser humano, ya que formaba parte de la dieta de sus dioses inmortales.',400,8,6),
 ('FR-47','Melocotonero Paraguayo',4,'',5,'Árbol caducifolio de porte bajo
con corteza lisa, de color ceniciento. Sus hojas son alargadas con el margen ligeramente aserrado, de
color verde brillante, algo más claras por el envés. El melocotonero está muy arraigado en la cultura
asiática.\r\nEn Japón, el noble heroe Momotaro, una especie de Cid japonés, nació del interior de un
enorme melocotón que flotaba río abajo.\r\nEn China se piensa que comer melocotón confiere
longevidad al ser humano, ya que formaba parte de la dieta de sus dioses inmortales.',400,8,6),
 ('FR-48','Nogal Común',4,'',3,'',400,9,7),
 ('FR-49','Parra Uva de Mesa',4,'',3,'',400,8,6),
 ('FR-5','Mandarino -Plantón joven',4,'',3,'',15,6,4),
 ('FR-50','Peral Castell',4,'',3,'Árbol piramidal, redondeado en su juventud,
luego oval, que llega hasta 20 metros de altura y por término medio vive 65 años.Tronco alto, grueso, de
corteza agrietada, gris, de la cual se destacan con frecuencia placas lenticulares.Las ramas se insertan
formando ángulo agudo con el tronco (45º), de corteza lisa, primero verde y luego gris-violácea, con
numerosas lenticelas.',400,8,6),
 ('FR-51','Peral Williams',4,'',3,'Árbol piramidal, redondeado en su
juventud, luego oval, que llega hasta 20 metros de altura y por término medio vive 65 años.Tronco alto,
grueso, de corteza agrietada, gris, de la cual se destacan con frecuencia placas lenticulares.Las ramas se
insertan formando ángulo agudo con el tronco (45º), de corteza lisa, primero verde y luego gris-violácea,
con numerosas lenticelas.',400,8,6),
 ('FR-52','Peral Conference',4,'',3,'Árbol piramidal, redondeado en su
juventud, luego oval, que llega hasta 20 metros de altura y por término medio vive 65 años.Tronco alto,
grueso, de corteza agrietada, gris, de la cual se destacan con frecuencia placas lenticulares.Las ramas se
insertan formando ángulo agudo con el tronco (45º), de corteza lisa, primero verde y luego gris-violácea,
con numerosas lenticelas.',400,8,6),
 ('FR-53','Peral Blanq. de Aranjuez',4,'',3,'Árbol piramidal, redondeado en
su juventud, luego oval, que llega hasta 20 metros de altura y por término medio vive 65 años.Tronco
alto, grueso, de corteza agrietada, gris, de la cual se destacan con frecuencia placas lenticulares.Las
ramas se insertan formando ángulo agudo con el tronco (45º), de corteza lisa, primero verde y luego
gris-violácea, con numerosas lenticelas.',400,8,6),
 ('FR-54','Níspero Tanaca',4,'',3,'Aunque originario del Sudeste de China, el
níspero llegó a Europa procedente de Japón en el siglo XVIII como árbol ornamental. En el siglo XIX se
inició el consumo de los frutos en toda el área mediterránea, donde se adaptó muy bien a las zonas de
cultivo de los cítricos.El cultivo intensivo comenzó a desarrollarse a finales de los años 60 y principios de
los 70, cuando comenzaron a implantarse las variedades y técnicas de cultivo actualmente
utilizadas.',400,9,7),
 ('FR-55','Olivo Cipresino',4,'',3,'Existen dos hipótesis sobre el origen del
olivo, una que postula que proviene de las costas de Siria, Líbano e Israel y otra que considera que lo
considera originario de Asia menor. La llegada a Europa probablemente tuvo lugar de mano de los
Fenicios, en transito por Chipre, Creta, e Islas del Mar Egeo, pasando a Grecia y más tarde a Italia. Los
primeros indicios de la presencia del olivo en las costas mediterráneas españolas coinciden con el
dominio romano, aunque fueron posteriormente los árabes los que impulsaron su cultivo en Andalucía,
convirtiendo a España en el primer país productr de aceite de oliva a nivel mundial.',400,8,6),
 ('FR-56','Nectarina',4,'',3,'',400,8,6),
 ('FR-57','Kaki Rojo Brillante',4,'',4,'De crecimiento algo lento los primeros
años, llega a alcanzar hasta doce metros de altura o más, aunque en cultivo se prefiere algo más bajo
(5-6). Tronco corto y copa extendida. Ramifica muy poco debido a la dominancia apical. Porte más o
menos piramidal, aunque con la edad se hace más globoso.',400,9,7),
 ('FR-58','Albaricoquero',4,'8/10',5,'árbol que puede pasar de los 6 m de
altura, en la región mediterránea con ramas formando una copa redondeada. La corteza del tronco es
pardo-violácea, agrietada; las ramas son rojizas y extendidas cuando jóvenes y las ramas secundarias son
cortas, divergentes y escasas. Las yemas latentes son frecuentes especialmente sobre las ramas
viejas.',200,11,8),
 ('FR-59','Albaricoquero',4,'10/12',5,'árbol que puede pasar de los 6 m de
altura, en la región mediterránea con ramas formando una copa redondeada. La corteza del tronco es
pardo-violácea, agrietada; las ramas son rojizas y extendidas cuando jóvenes y las ramas secundarias son
cortas, divergentes y escasas. Las yemas latentes son frecuentes especialmente sobre las ramas
viejas.',200,22,17),
 ('FR-6','Mandarino 2 años injerto',4,'',3,'',15,7,5),
 ('FR-60','Albaricoquero',4,'12/14',5,'árbol que puede pasar de los 6 m de
altura, en la región mediterránea con ramas formando una copa redondeada. La corteza del tronco es
pardo-violácea, agrietada; las ramas son rojizas y extendidas cuando jóvenes y las ramas secundarias son
cortas, divergentes y escasas. Las yemas latentes son frecuentes especialmente sobre las ramas
viejas.',200,32,25),
 ('FR-61','Albaricoquero',4,'14/16',5,'árbol que puede pasar de los 6 m de
altura, en la región mediterránea con ramas formando una copa redondeada. La corteza del tronco es
pardo-violácea, agrietada; las ramas son rojizas y extendidas cuando jóvenes y las ramas secundarias son
cortas, divergentes y escasas. Las yemas latentes son frecuentes especialmente sobre las ramas
viejas.',200,49,39),
 ('FR-62','Albaricoquero',4,'16/18',5,'árbol que puede pasar de los 6 m de
altura, en la región mediterránea con ramas formando una copa redondeada. La corteza del tronco es
pardo-violácea, agrietada; las ramas son rojizas y extendidas cuando jóvenes y las ramas secundarias son
cortas, divergentes y escasas. Las yemas latentes son frecuentes especialmente sobre las ramas
viejas.',200,70,56),
 ('FR-63','Cerezo',4,'8/10',7,'Las principales especies
de cerezo cultivadas en el mundo son el cerezo dulce (Prunus avium), el guindo (P. cerasus) y el cerezo
\"Duke\", híbrido de los anteriores. Ambas especies son naturales del sureste de Europa y oeste de Asia.
El cerezo dulce tuvo su origen probablemente en el mar Negro y en el mar Caspio, difundiéndose
después hacia Europa y Asia, llevado por los pájaros y las migraciones humanas. Fue uno de los frutales
más apreciados por los griegos y con el Imperio Romano se extendió a regiones muy diversas. En la
actualidad, el cerezo se encuentra difundido por numerosas regiones y países del mundo con clima
templado',300,11,8),
 ('FR-64','Cerezo',4,'10/12',7,'Las principales
especies de cerezo cultivadas en el mundo son el cerezo dulce (Prunus avium), el guindo (P. cerasus) y el
cerezo \"Duke\", híbrido de los anteriores. Ambas especies son naturales del sureste de Europa y oeste
de Asia. El cerezo dulce tuvo su origen probablemente en el mar Negro y en el mar Caspio,
difundiéndose después hacia Europa y Asia, llevado por los pájaros y las migraciones humanas. Fue uno
de los frutales más apreciados por los griegos y con el Imperio Romano se extendió a regiones muy
diversas. En la actualidad, el cerezo se encuentra difundido por numerosas regiones y países del mundo
con clima templado',15,22,17),
 ('FR-65','Cerezo',4,'12/14',7,'Las principales
especies de cerezo cultivadas en el mundo son el cerezo dulce (Prunus avium), el guindo (P. cerasus) y el
cerezo \"Duke\", híbrido de los anteriores. Ambas especies son naturales del sureste de Europa y oeste
de Asia. El cerezo dulce tuvo su origen probablemente en el mar Negro y en el mar Caspio,
difundiéndose después hacia Europa y Asia, llevado por los pájaros y las migraciones humanas. Fue uno
de los frutales más apreciados por los griegos y con el Imperio Romano se extendió a regiones muy
diversas. En la actualidad, el cerezo se encuentra difundido por numerosas regiones y países del mundo
con clima templado',200,32,25),
 ('FR-66','Cerezo',4,'14/16',7,'Las principales
especies de cerezo cultivadas en el mundo son el cerezo dulce (Prunus avium), el guindo (P. cerasus) y el
cerezo \"Duke\", híbrido de los anteriores. Ambas especies son naturales del sureste de Europa y oeste
de Asia. El cerezo dulce tuvo su origen probablemente en el mar Negro y en el mar Caspio,
difundiéndose después hacia Europa y Asia, llevado por los pájaros y las migraciones humanas. Fue uno
de los frutales más apreciados por los griegos y con el Imperio Romano se extendió a regiones muy
diversas. En la actualidad, el cerezo se encuentra difundido por numerosas regiones y países del mundo
con clima templado',50,49,39),
 ('FR-67','Cerezo',4,'16/18',7,'Las principales
especies de cerezo cultivadas en el mundo son el cerezo dulce (Prunus avium), el guindo (P. cerasus) y el
cerezo \"Duke\", híbrido de los anteriores. Ambas especies son naturales del sureste de Europa y oeste
de Asia. El cerezo dulce tuvo su origen probablemente en el mar Negro y en el mar Caspio,
difundiéndose después hacia Europa y Asia, llevado por los pájaros y las migraciones humanas. Fue uno
de los frutales más apreciados por los griegos y con el Imperio Romano se extendió a regiones muy
diversas. En la actualidad, el cerezo se encuentra difundido por numerosas regiones y países del mundo
con clima templado',50,70,56),
 ('FR-68','Cerezo',4,'18/20',7,'Las principales
especies de cerezo cultivadas en el mundo son el cerezo dulce (Prunus avium), el guindo (P. cerasus) y el
cerezo \"Duke\", híbrido de los anteriores. Ambas especies son naturales del sureste de Europa y oeste
de Asia. El cerezo dulce tuvo su origen probablemente en el mar Negro y en el mar Caspio,
difundiéndose después hacia Europa y Asia, llevado por los pájaros y las migraciones humanas. Fue uno
de los frutales más apreciados por los griegos y con el Imperio Romano se extendió a regiones muy
diversas. En la actualidad, el cerezo se encuentra difundido por numerosas regiones y países del mundo
con clima templado',50,80,64),
 ('FR-69','Cerezo',4,'20/25',7,'Las principales
especies de cerezo cultivadas en el mundo son el cerezo dulce (Prunus avium), el guindo (P. cerasus) y el
cerezo \"Duke\", híbrido de los anteriores. Ambas especies son naturales del sureste de Europa y oeste
de Asia. El cerezo dulce tuvo su origen probablemente en el mar Negro y en el mar Caspio,
difundiéndose después hacia Europa y Asia, llevado por los pájaros y las migraciones humanas. Fue uno
de los frutales más apreciados por los griegos y con el Imperio Romano se extendió a regiones muy
diversas. En la actualidad, el cerezo se encuentra difundido por numerosas regiones y países del mundo
con clima templado',50,91,72),
 ('FR-7','Mandarino calibre 8/10',4,'',3,'',15,29,23),
 ('FR-70','Ciruelo',4,'8/10',3,'árbol de tamaño mediano que alcanza una
altura máxima de 5-6 m. Tronco de corteza pardo-azulada, brillante, lisa o agrietada longitudinalmente.
Produce ramas alternas, pequeñas, delgadas, unas veces lisas, glabras y otras pubescentes y
vellosas',50,11,8),
 ('FR-71','Ciruelo',4,'10/12',3,'árbol de tamaño mediano que alcanza una
altura máxima de 5-6 m. Tronco de corteza pardo-azulada, brillante, lisa o agrietada longitudinalmente.
Produce ramas alternas, pequeñas, delgadas, unas veces lisas, glabras y otras pubescentes y
vellosas',50,22,17),
 ('FR-72','Ciruelo',4,'12/14',3,'árbol de tamaño mediano que alcanza una
altura máxima de 5-6 m. Tronco de corteza pardo-azulada, brillante, lisa o agrietada longitudinalmente.
Produce ramas alternas, pequeñas, delgadas, unas veces lisas, glabras y otras pubescentes y
vellosas',50,32,25),
 ('FR-73','Granado',4,'8/10',3,'pequeño árbol caducifolio, a veces con porte
arbustivo, de 3 a 6 m de altura, con el tronco retorcido. Madera dura y corteza escamosa de color
grisáceo. Las ramitas jóvenes son más o menos cuadrangulares o angostas y de cuatro alas,
posteriormente se vuelven redondas con corteza de color café grisáceo, la mayoría de las ramas, pero
especialmente las pequeñas ramitas axilares, son en forma de espina o terminan en una espina aguda; la
copa es extendida.',50,13,10),
 ('FR-74','Granado',4,'10/12',3,'pequeño árbol caducifolio, a veces con
porte arbustivo, de 3 a 6 m de altura, con el tronco retorcido. Madera dura y corteza escamosa de color
grisáceo. Las ramitas jóvenes son más o menos cuadrangulares o angostas y de cuatro alas,
posteriormente se vuelven redondas con corteza de color café grisáceo, la mayoría de las ramas, pero
especialmente las pequeñas ramitas axilares, son en forma de espina o terminan en una espina aguda; la
copa es extendida.',50,22,17),
 ('FR-75','Granado',4,'12/14',3,'pequeño árbol caducifolio, a veces con
porte arbustivo, de 3 a 6 m de altura, con el tronco retorcido. Madera dura y corteza escamosa de color
grisáceo. Las ramitas jóvenes son más o menos cuadrangulares o angostas y de cuatro alas,
posteriormente se vuelven redondas con corteza de color café grisáceo, la mayoría de las ramas, pero
especialmente las pequeñas ramitas axilares, son en forma de espina o terminan en una espina aguda; la
copa es extendida.',50,32,25),
 ('FR-76','Granado',4,'14/16',3,'pequeño árbol caducifolio, a veces con
porte arbustivo, de 3 a 6 m de altura, con el tronco retorcido. Madera dura y corteza escamosa de color
grisáceo. Las ramitas jóvenes son más o menos cuadrangulares o angostas y de cuatro alas,
posteriormente se vuelven redondas con corteza de color café grisáceo, la mayoría de las ramas, pero
especialmente las pequeñas ramitas axilares, son en forma de espina o terminan en una espina aguda; la
copa es extendida.',50,49,39),
 ('FR-77','Granado',4,'16/18',3,'pequeño árbol caducifolio, a veces con
porte arbustivo, de 3 a 6 m de altura, con el tronco retorcido. Madera dura y corteza escamosa de color
grisáceo. Las ramitas jóvenes son más o menos cuadrangulares o angostas y de cuatro alas,
posteriormente se vuelven redondas con corteza de color café grisáceo, la mayoría de las ramas, pero
especialmente las pequeñas ramitas axilares, son en forma de espina o terminan en una espina aguda; la
copa es extendida.',50,70,56),
 ('FR-78','Higuera',4,'8/10',3,'La higuera (Ficus carica L.) es un árbol típico
de secano en los países mediterráneos. Su rusticidad y su fácil multiplicación hacen de la higuera un
frutal muy apropiado para el cultivo extensivo.. Siempre ha sido considerado como árbol que no
requiere cuidado alguno una vez plantado y arraigado, limitándose el hombre a recoger de él los frutos
cuando maduran, unos para consumo en fresco y otros para conserva. Las únicas higueras con cuidados
culturales esmerados, en muchas comarcas, son las brevales, por el interés económico de su primera
cosecha, la de brevas.',50,15,12),
 ('FR-79','Higuera',4,'10/12',3,'La higuera (Ficus carica L.) es un árbol típico
de secano en los países mediterráneos. Su rusticidad y su fácil multiplicación hacen de la higuera un
frutal muy apropiado para el cultivo extensivo.. Siempre ha sido considerado como árbol que no
requiere cuidado alguno una vez plantado y arraigado, limitándose el hombre a recoger de él los frutos
cuando maduran, unos para consumo en fresco y otros para conserva. Las únicas higueras con cuidados
culturales esmerados, en muchas comarcas, son las brevales, por el interés económico de su primera
cosecha, la de brevas.',50,22,17),
 ('FR-8','Limonero -Plantón joven',4,'',4,'El limonero, pertenece al grupo de
los cítricos, teniendo su origen hace unos 20 millones de años en el sudeste asiático. Fue introducido por
los árabes en el área mediterránea entre los años 1.000 a 1.200, habiendo experimentando numerosas
modificaciones debidas tanto a la selección natural mediante hibridaciones espontáneas como a las
producidas por el',15,6,4),
 ('FR-80','Higuera',4,'12/14',3,'La higuera (Ficus carica L.) es un árbol típico
de secano en los países mediterráneos. Su rusticidad y su fácil multiplicación hacen de la higuera un
frutal muy apropiado para el cultivo extensivo.. Siempre ha sido considerado como árbol que no
requiere cuidado alguno una vez plantado y arraigado, limitándose el hombre a recoger de él los frutos
cuando maduran, unos para consumo en fresco y otros para conserva. Las únicas higueras con cuidados
culturales esmerados, en muchas comarcas, son las brevales, por el interés económico de su primera
cosecha, la de brevas.',50,32,25),
 ('FR-81','Higuera',4,'14/16',3,'La higuera (Ficus carica L.) es un árbol típico
de secano en los países mediterráneos. Su rusticidad y su fácil multiplicación hacen de la higuera un
frutal muy apropiado para el cultivo extensivo.. Siempre ha sido considerado como árbol que no
requiere cuidado alguno una vez plantado y arraigado, limitándose el hombre a recoger de él los frutos
cuando maduran, unos para consumo en fresco y otros para conserva. Las únicas higueras con cuidados
culturales esmerados, en muchas comarcas, son las brevales, por el interés económico de su primera
cosecha, la de brevas.',50,49,39),
 ('FR-82','Higuera',4,'16/18',3,'La higuera (Ficus carica L.) es un árbol típico
de secano en los países mediterráneos. Su rusticidad y su fácil multiplicación hacen de la higuera un
frutal muy apropiado para el cultivo extensivo.. Siempre ha sido considerado como árbol que no
requiere cuidado alguno una vez plantado y arraigado, limitándose el hombre a recoger de él los frutos
cuando maduran, unos para consumo en fresco y otros para conserva. Las únicas higueras con cuidados
culturales esmerados, en muchas comarcas, son las brevales, por el interés económico de su primera
cosecha, la de brevas.',50,70,56),
 ('FR-83','Higuera',4,'18/20',3,'La higuera (Ficus carica L.) es un árbol típico
de secano en los países mediterráneos. Su rusticidad y su fácil multiplicación hacen de la higuera un
frutal muy apropiado para el cultivo extensivo.. Siempre ha sido considerado como árbol que no
requiere cuidado alguno una vez plantado y arraigado, limitándose el hombre a recoger de él los frutos
cuando maduran, unos para consumo en fresco y otros para conserva. Las únicas higueras con cuidados
culturales esmerados, en muchas comarcas, son las brevales, por el interés económico de su primera
cosecha, la de brevas.',50,80,64),
 ('FR-84','Kaki',4,'8/10',4,'De crecimiento algo lento los primeros años, llega
a alcanzar hasta doce metros de altura o más, aunque en cultivo se prefiere algo más bajo (5-6). Tronco
corto y copa extendida. Ramifica muy poco debido a la dominancia apical. Porte más o menos piramidal,
aunque con la edad se hace más globoso.',50,13,10),
 ('FR-85','Kaki',4,'16/18',4,'De crecimiento algo lento los primeros años,
llega a alcanzar hasta doce metros de altura o más, aunque en cultivo se prefiere algo más bajo (5-6).
Tronco corto y copa extendida. Ramifica muy poco debido a la dominancia apical. Porte más o menos
piramidal, aunque con la edad se hace más globoso.',50,70,56),
 ('FR-86','Manzano',4,'8/10',3,'alcanza como máximo 10 m. de altura y
tiene una copa globosa. Tronco derecho que normalmente alcanza de 2 a 2,5 m. de altura, con corteza
cubierta de lenticelas, lisa, adherida, de color ceniciento verdoso sobre los ramos y escamosa y gris
parda sobre las partes viejas del árbol. Tiene una vida de unos 60-80 años. Las ramas se insertan en
ángulo abierto sobre el tallo, de color verde oscuro, a veces tendiendo a negruzco o violáceo. Los brotes
jóvenes terminan con frecuencia en una espina',50,11,8),
 ('FR-87','Manzano',4,'10/12',3,'alcanza como máximo 10 m. de altura y
tiene una copa globosa. Tronco derecho que normalmente alcanza de 2 a 2,5 m. de altura, con corteza
cubierta de lenticelas, lisa, adherida, de color ceniciento verdoso sobre los ramos y escamosa y gris
parda sobre las partes viejas del árbol. Tiene una vida de unos 60-80 años. Las ramas se insertan en
ángulo abierto sobre el tallo, de color verde oscuro, a veces tendiendo a negruzco o violáceo. Los brotes
jóvenes terminan con frecuencia en una espina',50,22,17),
 ('FR-88','Manzano',4,'12/14',3,'alcanza como máximo 10 m. de altura y
tiene una copa globosa. Tronco derecho que normalmente alcanza de 2 a 2,5 m. de altura, con corteza
cubierta de lenticelas, lisa, adherida, de color ceniciento verdoso sobre los ramos y escamosa y gris
parda sobre las partes viejas del árbol. Tiene una vida de unos 60-80 años. Las ramas se insertan en
ángulo abierto sobre el tallo, de color verde oscuro, a veces tendiendo a negruzco o violáceo. Los brotes
jóvenes terminan con frecuencia en una espina',50,32,25),
 ('FR-89','Manzano',4,'14/16',3,'alcanza como máximo 10 m. de altura y
tiene una copa globosa. Tronco derecho que normalmente alcanza de 2 a 2,5 m. de altura, con corteza
cubierta de lenticelas, lisa, adherida, de color ceniciento verdoso sobre los ramos y escamosa y gris
parda sobre las partes viejas del árbol. Tiene una vida de unos 60-80 años. Las ramas se insertan en
ángulo abierto sobre el tallo, de color verde oscuro, a veces tendiendo a negruzco o violáceo. Los brotes
jóvenes terminan con frecuencia en una espina',50,49,39),
 ('FR-9','Limonero calibre 8/10',4,'',4,'El limonero, pertenece al grupo de los
cítricos, teniendo su origen hace unos 20 millones de años en el sudeste asiático. Fue introducido por los
árabes en el área mediterránea entre los años 1.000 a 1.200, habiendo experimentando numerosas
modificaciones debidas tanto a la selección natural mediante hibridaciones espontáneas como a las
producidas por el',15,29,23),
 ('FR-90','Níspero',4,'16/18',3,'Aunque originario del Sudeste de China, el
níspero llegó a Europa procedente de Japón en el siglo XVIII como árbol ornamental. En el siglo XIX se
inició el consumo de los frutos en toda el área mediterránea, donde se adaptó muy bien a las zonas de
cultivo de los cítricos.El cultivo intensivo comenzó a desarrollarse a finales de los años 60 y principios de
los 70, cuando comenzaron a implantarse las variedades y técnicas de cultivo actualmente
utilizadas.',50,70,56),
 ('FR-91','Níspero',4,'18/20',3,'Aunque originario del Sudeste de China, el
níspero llegó a Europa procedente de Japón en el siglo XVIII como árbol ornamental. En el siglo XIX se
inició el consumo de los frutos en toda el área mediterránea, donde se adaptó muy bien a las zonas de
cultivo de los cítricos.El cultivo intensivo comenzó a desarrollarse a finales de los años 60 y principios de
los 70, cuando comenzaron a implantarse las variedades y técnicas de cultivo actualmente
utilizadas.',50,80,64),
 ('FR-92','Melocotonero',4,'8/10',5,'Árbol caducifolio de porte bajo con
corteza lisa, de color ceniciento. Sus hojas son alargadas con el margen ligeramente aserrado, de color
verde brillante, algo más claras por el envés. El melocotonero está muy arraigado en la cultura
asiática.\r\nEn Japón, el noble heroe Momotaro, una especie de Cid japonés, nació del interior de un
enorme melocotón que flotaba río abajo.\r\nEn China se piensa que comer melocotón confiere
longevidad al ser humano, ya que formaba parte de la dieta de sus dioses inmortales.',50,11,8),
 ('FR-93','Melocotonero',4,'10/12',5,'Árbol caducifolio de porte bajo con
corteza lisa, de color ceniciento. Sus hojas son alargadas con el margen ligeramente aserrado, de color
verde brillante, algo más claras por el envés. El melocotonero está muy arraigado en la cultura
asiática.\r\nEn Japón, el noble heroe Momotaro, una especie de Cid japonés, nació del interior de un
enorme melocotón que flotaba río abajo.\r\nEn China se piensa que comer melocotón confiere
longevidad al ser humano, ya que formaba parte de la dieta de sus dioses inmortales.',50,22,17),
 ('FR-94','Melocotonero',4,'12/14',5,'Árbol caducifolio de porte bajo con
corteza lisa, de color ceniciento. Sus hojas son alargadas con el margen ligeramente aserrado, de color
verde brillante, algo más claras por el envés. El melocotonero está muy arraigado en la cultura
asiática.\r\nEn Japón, el noble heroe Momotaro, una especie de Cid japonés, nació del interior de un
enorme melocotón que flotaba río abajo.\r\nEn China se piensa que comer melocotón confiere
longevidad al ser humano, ya que formaba parte de la dieta de sus dioses inmortales.',50,32,25),
 ('FR-95','Melocotonero',4,'14/16',5,'Árbol caducifolio de porte bajo con
corteza lisa, de color ceniciento. Sus hojas son alargadas con el margen ligeramente aserrado, de color
verde brillante, algo más claras por el envés. El melocotonero está muy arraigado en la cultura
asiática.\r\nEn Japón, el noble heroe Momotaro, una especie de Cid japonés, nació del interior de un
enorme melocotón que flotaba río abajo.\r\nEn China se piensa que comer melocotón confiere
longevidad al ser humano, ya que formaba parte de la dieta de sus dioses inmortales.',50,49,39),
 ('FR-96','Membrillero',4,'8/10',3,'arbolito caducifolio de 4-6 m de altura
con el tronco tortuoso y la corteza lisa, grisácea, que se desprende en escamas con la edad. Copa
irregular, con ramas inermes, flexuosas, parduzcas, punteadas. Ramillas jóvenes tomentosas',50,11,8),
 ('FR-97','Membrillero',4,'10/12',3,'arbolito caducifolio de 4-6 m de altura
con el tronco tortuoso y la corteza lisa, grisácea, que se desprende en escamas con la edad. Copa
irregular, con ramas inermes, flexuosas, parduzcas, punteadas. Ramillas jóvenes tomentosas',50,22,17),
 ('FR-98','Membrillero',4,'12/14',3,'arbolito caducifolio de 4-6 m de altura
con el tronco tortuoso y la corteza lisa, grisácea, que se desprende en escamas con la edad. Copa
irregular, con ramas inermes, flexuosas, parduzcas, punteadas. Ramillas jóvenes tomentosas',50,32,25),
 ('FR-99','Membrillero',4,'14/16',3,'arbolito caducifolio de 4-6 m de altura
con el tronco tortuoso y la corteza lisa, grisácea, que se desprende en escamas con la edad. Copa
irregular, con ramas inermes, flexuosas, parduzcas, punteadas. Ramillas jóvenes tomentosas',50,49,39),
 ('OR-001','Arbustos Mix Maceta',5,'40-60',8,'',25,5,4),
 ('OR-100','Mimosa Injerto CLASICA Dealbata ',5,'100-110',6,'Acacia
dealbata. Nombre común o vulgar: Mimosa fina, Mimosa, Mimosa común, Mimosa plateada, Aromo
francés. Familia: Mimosaceae. Origen: Australia, Sureste, (N. G. del Sur y Victoria). Arbol de follaje
persistente muy usado en parques por su atractiva floración amarilla hacia fines del invierno. Altura: de
3 a 10 metros generalmente. Crecimiento rápido. Follaje perenne de tonos plateados, muy ornamental.
Sus hojas son de textura fina, de color verde y sus flores amarillas que aparecen en racimos grandes.
Florece de Enero a Marzo (Hemisferio Norte). Legumbre de 5-9 cm de longitud, recta o ligeramente
curvada, con los bordes algo constreñidos entre las semillas, que se disponen en el fruto
longitudinalmente...',100,12,9),
 ('OR-101','Expositor Mimosa Semilla Mix',5,'170-200',6,'Acacia dealbata.
Nombre común o vulgar: Mimosa fina, Mimosa, Mimosa común, Mimosa plateada, Aromo francés.
Familia: Mimosaceae. Origen: Australia, Sureste, (N. G. del Sur y Victoria). Arbol de follaje persistente
muy usado en parques por su atractiva floración amarilla hacia fines del invierno. Altura: de 3 a 10
metros generalmente. Crecimiento rápido. Follaje perenne de tonos plateados, muy ornamental. Sus
hojas son de textura fina, de color verde y sus flores amarillas que aparecen en racimos grandes. Florece
de Enero a Marzo (Hemisferio Norte). Legumbre de 5-9 cm de longitud, recta o ligeramente curvada, con
los bordes algo constreñidos entre las semillas, que se disponen en el fruto
longitudinalmente...',100,6,4),
 ('OR-102','Mimosa Semilla Bayleyana ',5,'170-200',6,'Acacia dealbata.
Nombre común o vulgar: Mimosa fina, Mimosa, Mimosa común, Mimosa plateada, Aromo francés.
Familia: Mimosaceae. Origen: Australia, Sureste, (N. G. del Sur y Victoria). Arbol de follaje persistente
muy usado en parques por su atractiva floración amarilla hacia fines del invierno. Altura: de 3 a 10
metros generalmente. Crecimiento rápido. Follaje perenne de tonos plateados, muy ornamental. Sus
hojas son de textura fina, de color verde y sus flores amarillas que aparecen en racimos grandes. Florece
de Enero a Marzo (Hemisferio Norte). Legumbre de 5-9 cm de longitud, recta o ligeramente curvada, con
los bordes algo constreñidos entre las semillas, que se disponen en el fruto
longitudinalmente...',100,6,4),
 ('OR-103','Mimosa Semilla Bayleyana ',5,'200-225',6,'Acacia dealbata.
Nombre común o vulgar: Mimosa fina, Mimosa, Mimosa común, Mimosa plateada, Aromo francés.
Familia: Mimosaceae. Origen: Australia, Sureste, (N. G. del Sur y Victoria). Arbol de follaje persistente
muy usado en parques por su atractiva floración amarilla hacia fines del invierno. Altura: de 3 a 10
metros generalmente. Crecimiento rápido. Follaje perenne de tonos plateados, muy ornamental. Sus
hojas son de textura fina, de color verde y sus flores amarillas que aparecen en racimos grandes. Florece
de Enero a Marzo (Hemisferio Norte). Legumbre de 5-9 cm de longitud, recta o ligeramente curvada, con
los bordes algo constreñidos entre las semillas, que se disponen en el fruto
longitudinalmente...',100,10,8),
 ('OR-104','Mimosa Semilla Cyanophylla ',5,'200-225',6,'Acacia
dealbata. Nombre común o vulgar: Mimosa fina, Mimosa, Mimosa común, Mimosa plateada, Aromo
francés. Familia: Mimosaceae. Origen: Australia, Sureste, (N. G. del Sur y Victoria). Arbol de follaje
persistente muy usado en parques por su atractiva floración amarilla hacia fines del invierno. Altura: de
3 a 10 metros generalmente. Crecimiento rápido. Follaje perenne de tonos plateados, muy ornamental.
Sus hojas son de textura fina, de color verde y sus flores amarillas que aparecen en racimos grandes.
Florece de Enero a Marzo (Hemisferio Norte). Legumbre de 5-9 cm de longitud, recta o ligeramente
curvada, con los bordes algo constreñidos entre las semillas, que se disponen en el fruto
longitudinalmente...',100,10,8),
 ('OR-105','Mimosa Semilla Espectabilis ',5,'160-170',6,'Acacia dealbata.
Nombre común o vulgar: Mimosa fina, Mimosa, Mimosa común, Mimosa plateada, Aromo francés.
Familia: Mimosaceae. Origen: Australia, Sureste, (N. G. del Sur y Victoria). Arbol de follaje persistente
muy usado en parques por su atractiva floración amarilla hacia fines del invierno. Altura: de 3 a 10
metros generalmente. Crecimiento rápido. Follaje perenne de tonos plateados, muy ornamental. Sus
hojas son de textura fina, de color verde y sus flores amarillas que aparecen en racimos grandes. Florece
de Enero a Marzo (Hemisferio Norte). Legumbre de 5-9 cm de longitud, recta o ligeramente curvada, con
los bordes algo constreñidos entre las semillas, que se disponen en el fruto
longitudinalmente...',100,6,4),
 ('OR-106','Mimosa Semilla Longifolia ',5,'200-225',6,'Acacia dealbata.
Nombre común o vulgar: Mimosa fina, Mimosa, Mimosa común, Mimosa plateada, Aromo francés.
Familia: Mimosaceae. Origen: Australia, Sureste, (N. G. del Sur y Victoria). Arbol de follaje persistente
muy usado en parques por su atractiva floración amarilla hacia fines del invierno. Altura: de 3 a 10
metros generalmente. Crecimiento rápido. Follaje perenne de tonos plateados, muy ornamental. Sus
hojas son de textura fina, de color verde y sus flores amarillas que aparecen en racimos grandes. Florece
de Enero a Marzo (Hemisferio Norte). Legumbre de 5-9 cm de longitud, recta o ligeramente curvada, con
los bordes algo constreñidos entre las semillas, que se disponen en el fruto
longitudinalmente...',100,10,8),
 ('OR-107','Mimosa Semilla Floribunda 4 estaciones',5,'120-140',6,'Acacia
dealbata. Nombre común o vulgar: Mimosa fina, Mimosa, Mimosa común, Mimosa plateada, Aromo
francés. Familia: Mimosaceae. Origen: Australia, Sureste, (N. G. del Sur y Victoria). Arbol de follaje
persistente muy usado en parques por su atractiva floración amarilla hacia fines del invierno. Altura: de
3 a 10 metros generalmente. Crecimiento rápido. Follaje perenne de tonos plateados, muy ornamental.
Sus hojas son de textura fina, de color verde y sus flores amarillas que aparecen en racimos grandes.
Florece de Enero a Marzo (Hemisferio Norte). Legumbre de 5-9 cm de longitud, recta o ligeramente
curvada, con los bordes algo constreñidos entre las semillas, que se disponen en el fruto
longitudinalmente...',100,6,4),
 ('OR-108','Abelia Floribunda',5,'35-45',6,'',100,5,4),
 ('OR-109','Callistemom (Mix)',5,'35-45',6,'Limpitatubos. arbolito de 6-7 m
de altura. Ramas flexibles y colgantes (de ahí lo de \"llorón\")..',100,5,4),
 ('OR-110','Callistemom (Mix)',5,'40-60',6,'Limpitatubos. arbolito de 6-7 m
de altura. Ramas flexibles y colgantes (de ahí lo de \"llorón\")..',100,2,1),
 ('OR-111','Corylus Avellana \"Contorta\"',5,'35-45',6,'',100,5,4),
 ('OR-112','Escallonia (Mix)',5,'35-45',6,'',120,5,4),
 ('OR-113','Evonimus Emerald Gayeti',5,'35-45',6,'',120,5,4),
 ('OR-114','Evonimus Pulchellus',5,'35-45',6,'',120,5,4),
 ('OR-115','Forsytia Intermedia \"Lynwood\"',5,'35-45',6,'',120,7,5),
 ('OR-116','Hibiscus Syriacus \"Diana\" -Blanco Puro',5,'35-45',6,'Por su
capacidad de soportar podas, pueden ser fácilmente moldeadas como bonsái en el transcurso de pocos
años. Flores de muchos colores según la variedad, desde el blanco puro al rojo intenso, del amarillo al
anaranjado. La flor apenas dura 1 día, pero continuamente aparecen nuevas y la floración se prolonga
durante todo el periodo de crecimiento vegetativo.',120,7,5),
 ('OR-117','Hibiscus Syriacus \"Helene\" -Blanco-C.rojo',5,'35-45',6,'Por
su capacidad de soportar podas, pueden ser fácilmente moldeadas como bonsái en el transcurso de
pocos años. Flores de muchos colores según la variedad, desde el blanco puro al rojo intenso, del
amarillo al anaranjado. La flor apenas dura 1 día, pero continuamente aparecen nuevas y la floración se
prolonga durante todo el periodo de crecimiento vegetativo.',120,7,5),
 ('OR-118','Hibiscus Syriacus \"Pink Giant\" Rosa',5,'35-45',6,'Por su
capacidad de soportar podas, pueden ser fácilmente moldeadas como bonsái en el transcurso de pocos
años. Flores de muchos colores según la variedad, desde el blanco puro al rojo intenso, del amarillo al
anaranjado. La flor apenas dura 1 día, pero continuamente aparecen nuevas y la floración se prolonga
durante todo el periodo de crecimiento vegetativo.',120,7,5),
 ('OR-119','Laurus Nobilis Arbusto - Ramificado Bajo',5,'40-50',6,'',120,5,4),
 ('OR-120','Lonicera Nitida ',5,'35-45',6,'',120,5,4),
 ('OR-121','Lonicera Nitida \"Maigrum\"',5,'35-45',6,'',120,5,4),
 ('OR-122','Lonicera Pileata',5,'35-45',6,'',120,5,4),
 ('OR-123','Philadelphus \"Virginal\"',5,'35-45',6,'',120,5,4),
 ('OR-124','Prunus pisardii ',5,'35-45',6,'',120,5,4),
 ('OR-125','Viburnum Tinus \"Eve Price\"',5,'35-45',6,'',120,5,4),
 ('OR-126','Weigelia \"Bristol Ruby\"',5,'35-45',6,'',120,5,4),
 ('OR-127','Camelia japonica',5,'40-60',6,'Arbusto excepcional por su
floración otoñal, invernal o primaveral. Flores: Las flores son solitarias, aparecen en el ápice de cada
rama, y son con una corola simple o doble, y comprendiendo varios colores. Suelen medir unos 7-12 cm
de diÃ metro y tienen 5 sépalos y 5 pétalos. Estambres numerosos unidos en la mitad o en 2/3 de su
longitud.',50,7,5),
 ('OR-128','Camelia japonica ejemplar',5,'200-250',6,'Arbusto excepcional
por su floración otoñal, invernal o primaveral. Flores: Las flores son solitarias, aparecen en el ápice de
cada rama, y son con una corola simple o doble, y comprendiendo varios colores. Suelen medir unos
7-12 cm de diÃ metro y tienen 5 sépalos y 5 pétalos. Estambres numerosos unidos en la mitad o en 2/3
de su longitud.',50,98,78),
 ('OR-129','Camelia japonica ejemplar',5,'250-300',6,'Arbusto excepcional
por su floración otoñal, invernal o primaveral. Flores: Las flores son solitarias, aparecen en el ápice de
cada rama, y son con una corola simple o doble, y comprendiendo varios colores. Suelen medir unos
7-12 cm de diÃ metro y tienen 5 sépalos y 5 pétalos. Estambres numerosos unidos en la mitad o en 2/3
de su longitud.',50,110,88),
 ('OR-130','Callistemom COPA',5,'110/120',6,'Limpitatubos. arbolito de 6-7
m de altura. Ramas flexibles y colgantes (de ahí lo de \"llorón\")..',50,18,14),
 ('OR-131','Leptospermum formado PIRAMIDE',5,'80-100',6,'',50,18,14),
 ('OR-132','Leptospermum COPA',5,'110/120',6,'',50,18,14),
 ('OR-133','Nerium oleander-CALIDAD \"GARDEN\"',5,'40-45',6,'',50,2,1),
 ('OR-134','Nerium Oleander Arbusto GRANDE',5,'160-200',6,'',100,38,30),
 ('OR-135','Nerium oleander COPA Calibre 6/8',5,'50-60',6,'',100,5,4),
 ('OR-136','Nerium oleander ARBOL Calibre
8/10',5,'225-250',6,'',100,18,14),
 ('OR-137','ROSAL TREPADOR',5,'',6,'',100,4,3),
 ('OR-138','Camelia Blanco, Chrysler Rojo, Soraya Naranja,
',5,'',6,'',100,4,3),
 ('OR-139','Landora Amarillo, Rose Gaujard bicolor
blanco-rojo',5,'',6,'',100,4,3),
 ('OR-140','Kordes Perfect bicolor rojo-amarillo, Roundelay rojo
fuerte',5,'',6,'',100,4,3),
 ('OR-141','Pitimini rojo',5,'',6,'',100,4,3),
 ('OR-142','Solanum Jazminoide',5,'150-160',6,'',100,2,1),
 ('OR-143','Wisteria Sinensis azul, rosa, blanca',5,'',6,'',100,9,7),
 ('OR-144','Wisteria Sinensis INJERTADAS DECÃ“',5,'140-150',6,'',100,12,9),
 ('OR-145','Bougamvillea Sanderiana Tutor',5,'80-100',6,'',100,2,1),
 ('OR-146','Bougamvillea Sanderiana Tutor',5,'125-150',6,'',100,4,3),
 ('OR-147','Bougamvillea Sanderiana Tutor',5,'180-200',6,'',100,7,5),
 ('OR-148','Bougamvillea Sanderiana Espaldera',5,'45-50',6,'',100,7,5),
 ('OR-149','Bougamvillea Sanderiana Espaldera',5,'140-150',6,'',100,17,13),
 ('OR-150','Bougamvillea roja, naranja',5,'110-130',6,'',100,2,1),
 ('OR-151','Bougamvillea Sanderiana, 3 tut. piramide',5,'',6,'',100,6,4),
 ('OR-152','Expositor Árboles clima continental',5,'170-200',6,'',100,6,4),
 ('OR-153','Expositor Árboles clima mediterráneo',5,'170-200',6,'',100,6,4),
 ('OR-154','Expositor Árboles borde del mar',5,'170-200',6,'',100,6,4),
 ('OR-155','Acer Negundo ',5,'200-225',6,'',100,6,4),
 ('OR-156','Acer platanoides ',5,'200-225',6,'',100,10,8),
 ('OR-157','Acer Pseudoplatanus ',5,'200-225',6,'',100,10,8),
 ('OR-158','Brachychiton Acerifolius ',5,'200-225',6,'',100,6,4),
 ('OR-159','Brachychiton Discolor ',5,'200-225',6,'',100,6,4),
 ('OR-160','Brachychiton Rupestris',5,'170-200',6,'',100,10,8),
 ('OR-161','Cassia Corimbosa ',5,'200-225',6,'',100,6,4),
 ('OR-162','Cassia Corimbosa ',5,'200-225',6,'',100,10,8),
 ('OR-163','Chitalpa Summer Bells ',5,'200-225',6,'',80,10,8),
 ('OR-164','Erytrina Kafra',5,'170-180',6,'',80,6,4),
 ('OR-165','Erytrina Kafra',5,'200-225',6,'',80,10,8),
 ('OR-166','Eucalyptus Citriodora ',5,'170-200',6,'',80,6,4),
 ('OR-167','Eucalyptus Ficifolia ',5,'170-200',6,'',80,6,4),
 ('OR-168','Eucalyptus Ficifolia ',5,'200-225',6,'',80,10,8),
 ('OR-169','Hibiscus Syriacus Var. Injertadas 1 Tallo
',5,'170-200',6,'',80,12,9),
 ('OR-170','Lagunaria Patersonii ',5,'140-150',6,'',80,6,4),
 ('OR-171','Lagunaria Patersonii ',5,'200-225',6,'',80,10,8),
 ('OR-172','Lagunaria patersonii calibre 8/10',5,'200-225',6,'',80,18,14),
 ('OR-173','Morus Alba ',5,'200-225',6,'',80,6,4),
 ('OR-174','Morus Alba calibre 8/10',5,'200-225',6,'',80,18,14),
 ('OR-175','Platanus Acerifolia ',5,'200-225',6,'',80,10,8),
 ('OR-176','Prunus pisardii ',5,'200-225',6,'',80,10,8),
 ('OR-177','Robinia Pseudoacacia Casque Rouge
',5,'200-225',6,'',80,15,12),
 ('OR-178','Salix Babylonica Pendula ',5,'170-200',6,'',80,6,4),
 ('OR-179','Sesbania Punicea ',5,'170-200',6,'',80,6,4),
 ('OR-180','Tamarix Ramosissima Pink Cascade
',5,'170-200',6,'',80,6,4),
 ('OR-181','Tamarix Ramosissima Pink Cascade
',5,'200-225',6,'',80,10,8),
 ('OR-182','Tecoma Stands ',5,'200-225',6,'',80,6,4),
 ('OR-183','Tecoma Stands ',5,'200-225',6,'',80,10,8),
 ('OR-184','Tipuana Tipu ',5,'170-200',6,'',80,6,4),
 ('OR-185','Pleioblastus distichus-Bambú enano',5,'15-20',6,'',80,6,4),
 ('OR-186','Sasa palmata ',5,'20-30',6,'',80,6,4),
 ('OR-187','Sasa palmata ',5,'40-45',6,'',80,10,8),
 ('OR-188','Sasa palmata ',5,'50-60',6,'',80,25,20),
 ('OR-189','Phylostachys aurea',5,'180-200',6,'',80,22,17),
 ('OR-190','Phylostachys aurea',5,'250-300',6,'',80,32,25),
 ('OR-191','Phylostachys Bambusa Spectabilis',5,'180-200',6,'',80,24,19),
 ('OR-192','Phylostachys biseti',5,'160-170',6,'',80,22,17),
 ('OR-193','Phylostachys biseti',5,'160-180',6,'',80,20,16),
 ('OR-194','Pseudosasa japonica (Metake)',5,'225-250',6,'',80,20,16),
 ('OR-195','Pseudosasa japonica (Metake) ',5,'30-40',6,'',80,6,4),
 ('OR-196','Cedrus Deodara ',5,'80-100',6,'',80,10,8),
 ('OR-197','Cedrus Deodara \"Feeling Blue\"
Novedad',5,'rastrero',6,'',80,12,9),
 ('OR-198','Juniperus chinensis \"Blue Alps\"',5,'20-30',6,'',80,4,3),
 ('OR-199','Juniperus Chinensis Stricta',5,'20-30',6,'',80,4,3),
 ('OR-200','Juniperus horizontalis Wiltonii',5,'20-30',6,'',80,4,3),
 ('OR-201','Juniperus squamata \"Blue Star\"',5,'20-30',6,'',80,4,3),
 ('OR-202','Juniperus x media Phitzeriana verde',5,'20-30',6,'',80,4,3),
 ('OR-203','Pinus Canariensis',5,'80-100',6,'',80,10,8),
 ('OR-204','Pinus Halepensis',5,'160-180',6,'',80,10,8),
 ('OR-205','Pinus Pinea -Pino Piñonero',5,'70-80',6,'',80,10,8),
 ('OR-206','Thuja Esmeralda ',5,'80-100',6,'',80,5,4),
 ('OR-207','Tuja Occidentalis Woodwardii',5,'20-30',6,'',80,4,3),
 ('OR-208','Tuja orientalis \"Aurea nana\"',5,'20-30',6,'',80,4,3),
 ('OR-209','Archontophoenix Cunninghamiana',5,'80 - 100',6,'',80,10,8),
 ('OR-210','Beucarnea Recurvata',5,'130 - 150',6,'',2,39,31),
 ('OR-211','Beucarnea Recurvata',5,'180 - 200',6,'',5,59,47),
 ('OR-212','Bismarckia Nobilis',5,'200 - 220',6,'',4,217,173),
 ('OR-213','Bismarckia Nobilis',5,'240 - 260',6,'',4,266,212),
 ('OR-214','Brahea Armata',5,'45 - 60',6,'',0,10,8),
 ('OR-215','Brahea Armata',5,'120 - 140',6,'',100,112,89),
 ('OR-216','Brahea Edulis',5,'80 - 100',6,'',100,19,15),
 ('OR-217','Brahea Edulis',5,'140 - 160',6,'',100,64,51),
 ('OR-218','Butia Capitata',5,'70 - 90',6,'',100,25,20),
 ('OR-219','Butia Capitata',5,'90 - 110',6,'',100,29,23),
 ('OR-220','Butia Capitata',5,'90 - 120',6,'',100,36,28),
 ('OR-221','Butia Capitata',5,'85 - 105',6,'',100,59,47),
 ('OR-222','Butia Capitata',5,'130 - 150',6,'',100,87,69),
 ('OR-223','Chamaerops Humilis',5,'40 - 45',6,'',100,4,3),
 ('OR-224','Chamaerops Humilis',5,'50 - 60',6,'',100,7,5),
 ('OR-225','Chamaerops Humilis',5,'70 - 90',6,'',100,10,8),
 ('OR-226','Chamaerops Humilis',5,'115 - 130',6,'',100,38,30),
 ('OR-227','Chamaerops Humilis',5,'130 - 150',6,'',100,64,51),
 ('OR-228','Chamaerops Humilis \"Cerifera\"',5,'70 - 80',6,'',100,32,25),
 ('OR-229','Chrysalidocarpus Lutescens -ARECA',5,'130 -
150',6,'',100,22,17),
 ('OR-230','Cordyline Australis -DRACAENA',5,'190 - 210',6,'',100,38,30),
 ('OR-231','Cycas Revoluta',5,'55 - 65',6,'',100,15,12),
 ('OR-232','Cycas Revoluta',5,'80 - 90',6,'',100,34,27),
 ('OR-233','Dracaena Drago',5,'60 - 70',6,'',1,13,10),
 ('OR-234','Dracaena Drago',5,'130 - 150',6,'',2,64,51),
 ('OR-235','Dracaena Drago',5,'150 - 175',6,'',2,92,73),
 ('OR-236','Jubaea Chilensis',5,'',6,'',100,49,39),
 ('OR-237','Livistonia Australis',5,'100 - 125',6,'',50,19,15),
 ('OR-238','Livistonia Decipiens',5,'90 - 110',6,'',50,19,15),
 ('OR-239','Livistonia Decipiens',5,'180 - 200',6,'',50,49,39),
 ('OR-240','Phoenix Canariensis',5,'110 - 130',6,'',50,6,4),
 ('OR-241','Phoenix Canariensis',5,'180 - 200',6,'',50,19,15),
 ('OR-242','Rhaphis Excelsa',5,'80 - 100',6,'',50,21,16),
 ('OR-243','Rhaphis Humilis',5,'150- 170',6,'',50,64,51),
 ('OR-244','Sabal Minor',5,'60 - 75',6,'',50,11,8),
 ('OR-245','Sabal Minor',5,'120 - 140',6,'',50,34,27),
 ('OR-246','Trachycarpus Fortunei',5,'90 - 105',6,'',50,18,14),
 ('OR-247','Trachycarpus Fortunei',5,'250-300',6,'',2,462,369),
 ('OR-248','Washingtonia Robusta',5,'60 - 70',6,'',15,3,2),
 ('OR-249','Washingtonia Robusta',5,'130 - 150',6,'',15,5,4),
 ('OR-250','Yucca Jewel',5,'80 - 105',6,'',15,10,8),
 ('OR-251','Zamia Furfuracaea',5,'90 - 110',6,'',15,168,134),
 ('OR-99','Mimosa DEALBATA Gaulois Astier ',5,'200-225',6,'Acacia
dealbata. Nombre común o vulgar: Mimosa fina, Mimosa, Mimosa común, Mimosa plateada, Aromo
francés. Familia: Mimosaceae. Origen: Australia, Sureste, (N. G. del Sur y Victoria). Arbol de follaje
persistente muy usado en parques por su atractiva floración amarilla hacia fines del invierno. Altura: de
3 a 10 metros generalmente. Crecimiento rápido. Follaje perenne de tonos plateados, muy ornamental.
Sus hojas son de textura fina, de color verde y sus flores amarillas que aparecen en racimos grandes.
Florece de Enero a Marzo (Hemisferio Norte). Legumbre de 5-9 cm de longitud, recta o ligeramente
curvada, con los bordes algo constreñidos entre las semillas, que se disponen en el fruto
longitudinalmente...',100,14,11);


 INSERT INTO order_detail VALUES (1,'FR-67',10,70,3),
(1,'OR-127',40,4,1),
(1,'OR-141',25,4,2),
(1,'OR-241',15,19,4),
(1,'OR-99',23,14,5),
(2,'FR-4',3,29,6),
(2,'FR-40',7,8,7),
(2,'OR-140',50,4,3),
(2,'OR-141',20,5,2),
(2,'OR-159',12,6,5),
(2,'OR-227',67,64,1),
(2,'OR-247',5,462,4),
(3,'FR-48',120,9,6),
(3,'OR-122',32,5,4),
(3,'OR-123',11,5,5),
(3,'OR-213',30,266,1),
(3,'OR-217',15,65,2),
(3,'OR-218',24,25,3),
(4,'FR-31',12,8,7),
(4,'FR-34',42,8,6),
(4,'FR-40',42,9,8),
(4,'OR-152',3,6,5),
(4,'OR-155',4,6,3),
(4,'OR-156',17,9,4),
(4,'OR-157',38,10,2),
(4,'OR-222',21,59,1),
(8,'FR-106',3,11,1),
(8,'FR-108',1,32,2),
(8,'FR-11',10,100,3),
(9,'AR-001',80,1,3),
(9,'AR-008',450,1,2),
(9,'FR-106',80,8,1),
(9,'FR-69',15,91,2),
(10,'FR-82',5,70,2),
(10,'FR-91',30,75,1),
(10,'OR-234',5,64,3),
(11,'AR-006',180,1,3),
(11,'OR-247',80,8,1),
(12,'AR-009',290,1,1),
(13,'11679',5,14,1),
(13,'21636',12,14,2),
(13,'FR-11',5,100,3),
(14,'FR-100',8,11,2),
(14,'FR-13',13,57,1),
(15,'FR-84',4,13,3),
(15,'OR-101',2,6,2),
(15,'OR-156',6,10,1),
(15,'OR-203',9,10,4),
(16,'30310',12,12,1),
(16,'FR-36',10,9,2),
(17,'11679',5,14,1),
(17,'22225',5,12,3),
(17,'FR-37',5,9,2),
(17,'FR-64',5,22,4),
(17,'OR-136',5,18,5),
(18,'22225',4,12,2),
(18,'FR-22',2,4,1),
(18,'OR-159',10,6,3),
(19,'30310',9,12,5),
(19,'FR-23',6,8,4),
(19,'FR-75',1,32,2),
(19,'FR-84',5,13,1),
(19,'OR-208',20,4,3),
(20,'11679',14,14,1),
(20,'30310',8,12,2),
(21,'21636',5,14,3),
(21,'FR-18',22,4,1),
(21,'FR-53',3,8,2),
(22,'OR-240',1,6,1),
(23,'AR-002',110,1,4),
(23,'FR-107',50,22,3),
(23,'FR-85',4,70,2),
(23,'OR-249',30,5,1),
(24,'22225',3,15,1),
(24,'FR-1',4,7,4),
(24,'FR-23',2,7,2),
(24,'OR-241',10,20,3),
(25,'FR-77',15,69,1),
(25,'FR-9',4,30,3),
(25,'FR-94',10,30,2),
(26,'FR-15',9,25,3),
(26,'OR-188',4,25,1),
(26,'OR-218',14,25,2),
(27,'OR-101',22,6,2),
(27,'OR-102',22,6,3),
(27,'OR-186',40,6,1),
(28,'FR-11',8,99,3),
(28,'OR-213',3,266,2),
(28,'OR-247',1,462,1),
(29,'FR-82',4,70,4),
(29,'FR-9',4,28,1),
(29,'FR-94',20,31,5),
(29,'OR-129',2,111,2),
(29,'OR-160',10,9,3),
(30,'AR-004',10,1,6),
(30,'FR-108',2,32,2),
(30,'FR-12',2,19,3),
(30,'FR-72',4,31,5),
(30,'FR-89',10,45,1),
(30,'OR-120',5,5,4),
(31,'AR-009',25,2,3),
(31,'FR-102',1,20,1),
(31,'FR-4',6,29,2),
(32,'11679',1,14,4),
(32,'21636',4,15,5),
(32,'22225',1,15,3),
(32,'OR-128',29,100,2),
(32,'OR-193',5,20,1),
(33,'FR-17',423,2,4),
(33,'FR-29',120,8,3),
(33,'OR-214',212,10,2),
(33,'OR-247',150,462,1),
(34,'FR-3',56,7,4),
(34,'FR-7',12,29,3),
(34,'OR-172',20,18,1),
(34,'OR-174',24,18,2),
(35,'21636',12,14,4),
(35,'FR-47',55,8,3),
(35,'OR-165',3,10,2),
(35,'OR-181',36,10,1),
(35,'OR-225',72,10,5),
(36,'30310',4,12,2),
(36,'FR-1',2,7,3),
(36,'OR-147',6,7,4),
(36,'OR-203',1,12,5),
(36,'OR-99',15,13,1),
(37,'FR-105',4,70,1),
(37,'FR-57',203,8,2),
(37,'OR-176',38,10,3),
(38,'11679',5,14,1),
(38,'21636',2,14,2),
(39,'22225',3,12,1),
(39,'30310',6,12,2),
(40,'AR-001',4,1,1),
(40,'AR-002',8,1,2),
(41,'AR-003',5,1,1),
(41,'AR-004',5,1,2),
(42,'AR-005',3,1,1),
(42,'AR-006',1,1,2),
(43,'AR-007',9,1,1),
(44,'AR-008',5,1,1),
(45,'AR-009',6,1,1),
(45,'AR-010',4,1,2),
(46,'FR-1',4,7,1),
(46,'FR-10',8,7,2),
(47,'FR-100',9,11,1),
(47,'FR-101',5,13,2),
(48,'FR-102',1,18,1),
(48,'FR-103',1,25,2),
(48,'OR-234',50,64,1),
(48,'OR-236',45,49,2),
(48,'OR-237',50,19,3),
(49,'OR-204',50,10,1),
(49,'OR-205',10,10,2),
(49,'OR-206',5,5,3),
(50,'OR-225',12,10,1),
(50,'OR-226',15,38,2),
(50,'OR-227',44,64,3),
(51,'OR-209',50,10,1),
(51,'OR-210',80,39,2),
(51,'OR-211',70,59,3),
(53,'FR-2',1,7,1),
(53,'FR-85',1,70,3),
(53,'FR-86',2,11,2),
(53,'OR-116',6,7,4),
(54,'11679',3,14,3),
(54,'FR-100',45,10,2),
(54,'FR-18',5,4,1),
(54,'FR-79',3,22,4),
(54,'OR-116',8,7,6),
(54,'OR-123',3,5,5),
(54,'OR-168',2,10,7),
(55,'OR-115',9,7,1),
(55,'OR-213',2,266,2),
(55,'OR-227',6,64,5),
(55,'OR-243',2,64,4),
(55,'OR-247',1,462,3),
(56,'OR-129',1,115,5),
(56,'OR-130',10,18,6),
(56,'OR-179',1,6,3),
(56,'OR-196',3,10,4),
(56,'OR-207',4,4,2),
(56,'OR-250',3,10,1),
(57,'FR-69',6,91,4),
(57,'FR-81',3,49,3),
(57,'FR-84',2,13,1),
(57,'FR-94',6,9,2),
(58,'OR-102',65,18,3),
(58,'OR-139',80,4,1),
(58,'OR-172',69,15,2),
(58,'OR-177',150,15,4),
(74,'FR-67',15,70,1),
(74,'OR-227',34,64,2),
(74,'OR-247',42,8,3),
(75,'AR-006',60,1,2),
(75,'FR-87',24,22,3),
(75,'OR-157',46,10,1),
(76,'AR-009',250,1,5),
(76,'FR-79',40,22,3),
(76,'FR-87',24,22,4),
(76,'FR-94',35,9,1),
(76,'OR-196',25,10,2),
(77,'22225',34,12,2),
(77,'30310',15,12,1),
(78,'FR-53',25,8,2),
(78,'FR-85',56,70,3),
(78,'OR-157',42,10,4),
(78,'OR-208',30,4,1),
(79,'OR-240',50,6,1),
(80,'FR-11',40,100,3),
(80,'FR-36',47,9,2),
(80,'OR-136',75,18,1),
(81,'OR-208',30,4,1),
(82,'OR-227',34,64,1),
(83,'OR-208',30,4,1),
(89,'FR-108',3,32,2),
(89,'FR-3',15,7,6),
(89,'FR-42',12,8,4),
(89,'FR-66',5,49,1),
(89,'FR-87',4,22,3),
(89,'OR-157',8,10,5),
(90,'AR-001',19,1,1),
(90,'AR-002',10,1,2),
(90,'AR-003',12,1,3),
(91,'FR-100',52,11,1),
(91,'FR-101',14,13,2),
(91,'FR-102',35,18,3),
(92,'FR-108',12,23,1),
(92,'FR-11',20,100,2),
(92,'FR-12',30,21,3),
(93,'FR-54',25,9,1),
(93,'FR-58',51,11,2),
(93,'FR-60',3,32,3),
(94,'11679',12,14,1),
(94,'FR-11',33,100,3),
(94,'FR-4',79,29,2),
(95,'FR-10',9,7,2),
(95,'FR-75',6,32,1),
(95,'FR-82',5,70,3),
(96,'FR-43',6,8,1),
(96,'FR-6',16,7,4),
(96,'FR-71',10,22,3),
(96,'FR-90',4,70,2),
(97,'FR-41',12,8,1),
(97,'FR-54',14,9,2),
(97,'OR-156',10,10,3),
(98,'FR-33',14,8,4),
(98,'FR-56',16,8,3),
(98,'FR-60',8,32,1),
(98,'FR-8',18,6,5),
(98,'FR-85',6,70,2),
(99,'OR-157',15,10,2),
(99,'OR-227',30,64,1),
(100,'FR-87',20,22,1),
(100,'FR-94',40,9,2),
(101,'AR-006',50,1,1),
(101,'AR-009',159,1,2),
(102,'22225',32,12,2),
(102,'30310',23,12,1),
(103,'FR-53',12,8,2),
(103,'OR-208',52,4,1),
(104,'FR-85',9,70,1),
(104,'OR-157',113,10,2),
(105,'OR-227',21,64,2),
(105,'OR-240',27,6,1),
(106,'AR-009',231,1,1),
(106,'OR-136',47,18,2),
(107,'30310',143,12,2),
(107,'FR-11',15,100,1),
(108,'FR-53',53,8,1),
(108,'OR-208',59,4,2),
(109,'FR-22',8,4,5),
(109,'FR-36',12,9,3),
(109,'FR-45',14,8,4),
(109,'OR-104',20,10,1),
(109,'OR-119',10,5,2),
(109,'OR-125',3,5,6),
(109,'OR-130',2,18,7),
(110,'AR-010',6,1,3),
(110,'FR-1',14,7,1),
(110,'FR-16',1,45,2),
(116,'21636',5,14,1),
(116,'AR-001',32,1,2),
(116,'AR-005',18,1,5),
(116,'FR-33',13,8,3),
(116,'OR-200',10,4,4),
(117,'FR-78',2,15,1),
(117,'FR-80',1,32,3),
(117,'OR-146',17,4,2),
(117,'OR-179',4,6,4),
(128,'AR-004',15,1,1),
(128,'OR-150',18,2,2),
(52,'FR-67',10,70,1),
(59,'FR-67',10,70,1),
(60,'FR-67',10,70,1),
(61,'FR-67',10,70,1),
(62,'FR-67',10,70,1),
(63,'FR-67',10,70,1),
(64,'FR-67',10,70,1),
(65,'FR-67',10,70,1),
(66,'FR-67',10,70,1),
(67,'FR-67',10,70,1),
(68,'FR-67',10,70,1),
(111,'FR-67',10,70,1),
(112,'FR-67',10,70,1),
(113,'FR-67',10,70,1),
(114,'FR-67',10,70,1),
(115,'FR-67',10,70,1),
(118,'FR-67',10,70,1),
(119,'FR-67',10,70,1),
(120,'FR-67',10,70,1),
(121,'FR-67',10,70,1),
(122,'FR-67',10,70,1),
(123,'FR-67',10,70,1),
(124,'FR-67',10,70,1),
(125,'FR-67',10,70,1),
(126,'FR-67',10,70,1),
(127,'FR-67',10,70,1);

