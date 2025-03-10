CREATE DATABASE IF NOT EXISTS Producers;
USE Producers;

CREATE TABLE Product(
    Product_id INT AUTO_INCREMENT,
    name varchar(100) NOT NULL,
    -- MAX VALUE  99.999.999,99
    price DECIMAL(10,2) NULL,
    stock int NULL DEFAULT 0,
    -- MAX VALUE 999,99
    specific_discount DECIMAL(5,2) NULL,
    CONSTRAINT PK_PRODUCT_ID PRIMARY KEY (Product_id),
    CONSTRAINT CHK_SPECIFIC_DISCOUNT CHECK (specific_discount BETWEEN 0 AND 100),
    CONSTRAINT CHK_PRICE CHECK (price>=0),
    CONSTRAINT CHK_STOCK CHECK (stock>=0)
);

CREATE TABLE `Order`(
    Order_id INT NOT NULL AUTO_INCREMENT,
    -- MAX VALUE  99.999.999,99
    total DECIMAL(10,2) NULL,
    client_name VARCHAR(100) NULL,
    payment_method VARCHAR(50) NOT NULL,
    payment_date DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT PK_ORDER_ID PRIMARY KEY (Order_id),
    CONSTRAINT CHK_TOTAL CHECK ( total>=0 )
);

CREATE TABLE Product_Order(
    Product_id INT NOT NULL,
    Order_id INT NOT NULL,
    amount INT NULL,
    CONSTRAINT FK_PRODUCT_ID FOREIGN KEY (Product_id) REFERENCES Product(Product_id),
    CONSTRAINT FK_ORDER_ID FOREIGN KEY (Order_id) REFERENCES `Order`(Order_id),
    CONSTRAINT CHK_AMOUNT CHECK (amount>=0),
    CONSTRAINT PK_PRODUCT_ORDER_ID PRIMARY KEY (Product_id, Order_id)
);


INSERT INTO Product (name, price, stock, specific_discount) VALUES
                                                                ('Laptop Gamer', 2500.00, 15, 10.00),
                                                                ('Mouse Inalámbrico', 35.99, 50, 5.00),
                                                                ('Teclado Mecánico', 120.50, 30, 8.50),
                                                                ('Monitor 27 Pulgadas', 450.00, 20, 12.00),
                                                                ('Silla Ergonómica', 320.75, 10, 7.50);

INSERT INTO `Order` (total, client_name, payment_method) VALUES
                                                             (2595.99, 'Juan Pérez', 'Tarjeta de crédito'),
                                                             (85.50, 'Ana Gómez', 'Efectivo'),
                                                             (920.00, 'Carlos López', 'PayPal'),
                                                             (320.75, 'María Rodríguez', 'Transferencia bancaria'),
                                                             (150.00, 'Luis Fernández', 'Efectivo');

INSERT INTO Product_Order (Product_id, Order_id, amount) VALUES
                                                             (1, 1, 1),  -- Laptop Gamer en la orden 1
                                                             (2, 2, 2),  -- 2 Mouse Inalámbricos en la orden 2
                                                             (3, 2, 1),  -- 1 Teclado Mecánico en la orden 2
                                                             (4, 3, 2),  -- 2 Monitores en la orden 3
                                                             (5, 4, 1),  -- 1 Silla Ergonómica en la orden 4
                                                             (3, 5, 1);  -- 1 Teclado Mecánico en la orden 5
