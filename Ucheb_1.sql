CREATE DATABASE `ucheb_1`  ;
USE `ucheb_1` ;

--
-- Table `courier`
--
CREATE TABLE `items` (
  `item_id` int NOT NULL AUTO_INCREMENT,
  `item_name` varchar(25) NULL,
  `item_desc` varchar(120) NULL,
  `item_cat` int NULL,
  `item_warehouse` int NULL,
  `item_amount` int NULL,
  PRIMARY KEY (`item_id`)

);


CREATE TABLE `category` (
  `cat_id` int NOT NULL AUTO_INCREMENT,
  `cat_name` varchar(20) NULL,
  PRIMARY KEY (`cat_id`)
);


CREATE TABLE `warehouse` (
  `warehouse_id` int NOT NULL AUTO_INCREMENT,
  `warehouse_adress` varchar(120) NULL,
  `warehouse_owner` varchar(60) NULL,
  PRIMARY KEY (`warehouse_id`)
);


ALTER TABLE `items` ADD FOREIGN KEY (`item_cat`) REFERENCES `category` (`cat_id`);

ALTER TABLE `items` ADD FOREIGN KEY (`item_warehouse`) REFERENCES `warehouse` (`warehouse_id`);

INSERT INTO `category` VALUES (1,'Скобяные изделия'),(2,'Обои'),(3,'Лаки и краски'),(4,'Кафель'),(5,'Двери межкомнатные'),(6,'Двери входные');

INSERT INTO `warehouse` VALUES (1,'г. Новосибирск, ул. Индустриальная, 5/4','Бондарев Илья Владимирович'),(2,'г. Новосибирск, ул. Машиностроителей, 1/2а','Ивченко Клавдия Михайловна');

INSERT INTO `items` VALUES (1,'Ручка-скоба УФ РС-105','Материал - сталь, производство - Россия',1,1,2500),(3,'Дверное полотно','Бренд Olovi, белое, глухое, 200*90',4,2,150),(4,'Гвозди строительные','4x100 мм без покрытия 1 кг',1,1,2000);
