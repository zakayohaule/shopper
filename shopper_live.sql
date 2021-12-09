-- MySQL dump 10.13  Distrib 8.0.27, for Linux (x86_64)
--
-- Host: localhost    Database: shopper
-- ------------------------------------------------------
-- Server version	8.0.27-0ubuntu0.20.04.1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__EFMigrationsHistory`
--

DROP TABLE IF EXISTS `__EFMigrationsHistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__EFMigrationsHistory` (
                                         `MigrationId` varchar(95) NOT NULL,
                                         `ProductVersion` varchar(32) NOT NULL,
                                         PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__EFMigrationsHistory`
--

LOCK TABLES `__EFMigrationsHistory` WRITE;
/*!40000 ALTER TABLE `__EFMigrationsHistory` DISABLE KEYS */;
INSERT INTO `__EFMigrationsHistory` VALUES ('20210106092958_Initial','3.1.4'),('20210108111108_RemovedUniqueIndexInSomeModels','3.1.4'),('20210117065649_AddReceiveLowStockAlertInSkuTable','3.1.4'),('20210117072406_RemoveReceiveLowStockAlertMigration','3.1.4'),('20210117183556_AddProductIdInSalesModel','3.1.4'),('20210118065806_AddUserIdInExpenseTable','3.1.4');
/*!40000 ALTER TABLE `__EFMigrationsHistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `attribute_options`
--

DROP TABLE IF EXISTS `attribute_options`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `attribute_options` (
                                     `id` smallint unsigned NOT NULL AUTO_INCREMENT,
                                     `tenant_id` char(36) NOT NULL,
                                     `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                                     `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                                     `name` longtext NOT NULL,
                                     `attribute_id` smallint unsigned NOT NULL,
                                     PRIMARY KEY (`id`),
                                     KEY `IX_attribute_options_attribute_id` (`attribute_id`),
                                     KEY `IX_attribute_options_tenant_id` (`tenant_id`),
                                     CONSTRAINT `FK_attribute_options_attributes_attribute_id` FOREIGN KEY (`attribute_id`) REFERENCES `attributes` (`id`) ON DELETE CASCADE,
                                     CONSTRAINT `FK_attribute_options_tenants_tenant_id` FOREIGN KEY (`tenant_id`) REFERENCES `tenants` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `attribute_options`
--

LOCK TABLES `attribute_options` WRITE;
/*!40000 ALTER TABLE `attribute_options` DISABLE KEYS */;
/*!40000 ALTER TABLE `attribute_options` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `attributes`
--

DROP TABLE IF EXISTS `attributes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `attributes` (
                              `id` smallint unsigned NOT NULL AUTO_INCREMENT,
                              `tenant_id` char(36) NOT NULL,
                              `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                              `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                              `name` longtext NOT NULL,
                              PRIMARY KEY (`id`),
                              KEY `IX_attributes_tenant_id` (`tenant_id`),
                              CONSTRAINT `FK_attributes_tenants_tenant_id` FOREIGN KEY (`tenant_id`) REFERENCES `tenants` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `attributes`
--

LOCK TABLES `attributes` WRITE;
/*!40000 ALTER TABLE `attributes` DISABLE KEYS */;
/*!40000 ALTER TABLE `attributes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `expenditure_types`
--

DROP TABLE IF EXISTS `expenditure_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `expenditure_types` (
                                     `id` smallint unsigned NOT NULL AUTO_INCREMENT,
                                     `tenant_id` char(36) NOT NULL,
                                     `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                                     `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                                     `name` longtext NOT NULL,
                                     PRIMARY KEY (`id`),
                                     KEY `IX_expenditure_types_tenant_id` (`tenant_id`),
                                     CONSTRAINT `FK_expenditure_types_tenants_tenant_id` FOREIGN KEY (`tenant_id`) REFERENCES `tenants` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `expenditure_types`
--

LOCK TABLES `expenditure_types` WRITE;
/*!40000 ALTER TABLE `expenditure_types` DISABLE KEYS */;
/*!40000 ALTER TABLE `expenditure_types` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `expenditures`
--

DROP TABLE IF EXISTS `expenditures`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `expenditures` (
                                `id` bigint unsigned NOT NULL AUTO_INCREMENT,
                                `tenant_id` char(36) NOT NULL,
                                `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                                `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                                `amount` int unsigned NOT NULL,
                                `expenditure_date` datetime(6) NOT NULL,
                                `description` longtext,
                                `expenditure_type_id` smallint unsigned NOT NULL,
                                `user_id` bigint DEFAULT NULL,
                                PRIMARY KEY (`id`),
                                KEY `IX_expenditures_expenditure_type_id` (`expenditure_type_id`),
                                KEY `IX_expenditures_tenant_id` (`tenant_id`),
                                KEY `IX_expenditures_user_id` (`user_id`),
                                CONSTRAINT `FK_expenditures_expenditure_types_expenditure_type_id` FOREIGN KEY (`expenditure_type_id`) REFERENCES `expenditure_types` (`id`) ON DELETE CASCADE,
                                CONSTRAINT `FK_expenditures_tenants_tenant_id` FOREIGN KEY (`tenant_id`) REFERENCES `tenants` (`id`) ON DELETE CASCADE,
                                CONSTRAINT `FK_expenditures_users_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE RESTRICT
) ENGINE=InnoDB;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `expenditures`
--

LOCK TABLES `expenditures` WRITE;
/*!40000 ALTER TABLE `expenditures` DISABLE KEYS */;
/*!40000 ALTER TABLE `expenditures` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `expirations`
--

DROP TABLE IF EXISTS `expirations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `expirations` (
                               `id` bigint unsigned NOT NULL AUTO_INCREMENT,
                               `tenant_id` char(36) NOT NULL,
                               `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                               `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                               `sku_Id` bigint unsigned NOT NULL,
                               `expiration_date` datetime(6) NOT NULL,
                               PRIMARY KEY (`id`),
                               UNIQUE KEY `IX_expirations_sku_Id` (`sku_Id`),
                               KEY `IX_expirations_tenant_id` (`tenant_id`),
                               CONSTRAINT `FK_expirations_skus_sku_Id` FOREIGN KEY (`sku_Id`) REFERENCES `skus` (`id`) ON DELETE CASCADE,
                               CONSTRAINT `FK_expirations_tenants_tenant_id` FOREIGN KEY (`tenant_id`) REFERENCES `tenants` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `expirations`
--

LOCK TABLES `expirations` WRITE;
/*!40000 ALTER TABLE `expirations` DISABLE KEYS */;
/*!40000 ALTER TABLE `expirations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `modules`
--

DROP TABLE IF EXISTS `modules`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `modules` (
                           `id` smallint unsigned NOT NULL AUTO_INCREMENT,
                           `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                           `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                           `name` varchar(255) DEFAULT NULL,
                           PRIMARY KEY (`id`),
                           UNIQUE KEY `IX_modules_name` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=15;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `modules`
--

LOCK TABLES `modules` WRITE;
/*!40000 ALTER TABLE `modules` DISABLE KEYS */;
INSERT INTO `modules` VALUES (1,'2021-10-29 14:09:32.689304','2021-10-29 14:09:34.089728','User Management'),(2,'2021-10-29 14:09:32.689348','2021-10-29 14:09:34.090327','Role Management'),(3,'2021-10-29 14:09:32.689348','2021-10-29 14:09:34.090882','Product Group Management'),(4,'2021-10-29 14:09:32.689348','2021-10-29 14:09:34.091056','Product Category Management'),(5,'2021-10-29 14:09:32.689349','2021-10-29 14:09:34.091201','Product Type Management'),(6,'2021-10-29 14:09:32.689358','2021-10-29 14:09:34.091341','Attribute Management'),(7,'2021-10-29 14:09:32.689358','2021-10-29 14:09:34.091464','Price Type Management'),(8,'2021-10-29 14:09:32.689358','2021-10-29 14:09:34.091577','Product Management'),(9,'2021-10-29 14:09:32.689359','2021-10-29 14:09:34.091785','Stock Management'),(10,'2021-10-29 14:09:32.689359','2021-10-29 14:09:34.091937','Sale Management'),(11,'2021-10-29 14:09:32.689359','2021-10-29 14:09:34.092053','Expenditure Management'),(12,'2021-10-29 14:09:32.689359','2021-10-29 14:09:34.092163','Business Info Management'),(13,'2021-10-29 14:09:32.689360','2021-10-29 14:09:34.092276','Dashboard'),(14,'2021-10-29 14:09:32.689360','2021-10-29 14:09:34.092385','Reports');
/*!40000 ALTER TABLE `modules` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `permissions`
--

DROP TABLE IF EXISTS `permissions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `permissions` (
                               `id` smallint unsigned NOT NULL AUTO_INCREMENT,
                               `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                               `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                               `name` varchar(255) DEFAULT NULL,
                               `display_name` varchar(255) DEFAULT NULL,
                               `module_id` smallint unsigned NOT NULL,
                               PRIMARY KEY (`id`),
                               UNIQUE KEY `IX_permissions_display_name` (`display_name`),
                               UNIQUE KEY `IX_permissions_name` (`name`),
                               KEY `IX_permissions_module_id` (`module_id`),
                               CONSTRAINT `FK_permissions_modules_module_id` FOREIGN KEY (`module_id`) REFERENCES `modules` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=66;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `permissions`
--

LOCK TABLES `permissions` WRITE;
/*!40000 ALTER TABLE `permissions` DISABLE KEYS */;
INSERT INTO `permissions` VALUES (1,'2021-10-29 14:09:34.218817','2021-10-29 14:09:34.330037','product_category_add','Add Product Categories',4),(2,'2021-10-29 14:09:34.218824','2021-10-29 14:09:34.330597','product_group_view','View Product Groups',3),(3,'2021-10-29 14:09:34.218824','2021-10-29 14:09:34.330856','product_group_edit','Edit Product Groups',3),(4,'2021-10-29 14:09:34.218824','2021-10-29 14:09:34.331030','product_group_delete','Delete Product Groups',3),(5,'2021-10-29 14:09:34.218825','2021-10-29 14:09:34.331194','expenditure_type_add','Add Expenditure Types',11),(6,'2021-10-29 14:09:34.218825','2021-10-29 14:09:34.331351','expenditure_type_view','View Expenditure Types',11),(7,'2021-10-29 14:09:34.218825','2021-10-29 14:09:34.331520','expenditure_type_edit','Edit Expenditure Types',11),(8,'2021-10-29 14:09:34.218825','2021-10-29 14:09:34.331666','expenditure_type_delete','Delete Expenditure Types',11),(9,'2021-10-29 14:09:34.218826','2021-10-29 14:09:34.331833','expenditure_add','Add Expenditure',11),(10,'2021-10-29 14:09:34.218826','2021-10-29 14:09:34.332015','expenditure_view','View Expenditure',11),(11,'2021-10-29 14:09:34.218826','2021-10-29 14:09:34.332170','expenditure_edit','Edit Expenditure',11),(12,'2021-10-29 14:09:34.218826','2021-10-29 14:09:34.332313','expenditure_delete','Delete Expenditure',11),(13,'2021-10-29 14:09:34.218826','2021-10-29 14:09:34.332446','product_add','Add Products',8),(14,'2021-10-29 14:09:34.218827','2021-10-29 14:09:34.332576','product_view','View Products',8),(15,'2021-10-29 14:09:34.218827','2021-10-29 14:09:34.332704','product_edit','Edit Products',8),(16,'2021-10-29 14:09:34.218827','2021-10-29 14:09:34.332860','product_delete','Delete Products',8),(17,'2021-10-29 14:09:34.218827','2021-10-29 14:09:34.333016','stock_add','Add Stock Keeping Item',9),(18,'2021-10-29 14:09:34.218827','2021-10-29 14:09:34.344626','stock_view','View Stock Keeping Item',9),(19,'2021-10-29 14:09:34.218828','2021-10-29 14:09:34.345078','stock_edit','Edit Stock Keeping Item',9),(20,'2021-10-29 14:09:34.218828','2021-10-29 14:09:34.345473','stock_delete','Delete Stock Keeping Item',9),(21,'2021-10-29 14:09:34.218828','2021-10-29 14:09:34.345684','sale_sell','Sell Product',10),(22,'2021-10-29 14:09:34.218828','2021-10-29 14:09:34.345869','sale_edit','Edit Sale',10),(23,'2021-10-29 14:09:34.218828','2021-10-29 14:09:34.346043','sale_record','Record Sale',10),(24,'2021-10-29 14:09:34.218829','2021-10-29 14:09:34.346238','sale_view','View Sales',10),(25,'2021-10-29 14:09:34.218829','2021-10-29 14:09:34.346501','sale_invoice_view','View Sale Invoices',10),(26,'2021-10-29 14:09:34.218829','2021-10-29 14:09:34.348991','sale_invoice_change_date','Change Invoice Date',10),(27,'2021-10-29 14:09:34.218829','2021-10-29 14:09:34.349289','business_info_view','View Business Information',12),(28,'2021-10-29 14:09:34.218829','2021-10-29 14:09:34.349572','business_info_update','Update Business Information',12),(29,'2021-10-29 14:09:34.218830','2021-10-29 14:09:34.349940','summary_view','View Summaries',13),(30,'2021-10-29 14:09:34.218830','2021-10-29 14:09:34.350485','stock_and_expiration_summary_view','View Stock And Expiration Summaries',13),(31,'2021-10-29 14:09:34.218824','2021-10-29 14:09:34.350704','product_group_add','Add Product Groups',3),(32,'2021-10-29 14:09:34.218830','2021-10-29 14:09:34.350977','sales_graph','View Sales Graphs',13),(33,'2021-10-29 14:09:34.218823','2021-10-29 14:09:34.351179','price_type_delete','Delete Price Types',7),(34,'2021-10-29 14:09:34.218823','2021-10-29 14:09:34.351369','price_type_view','View Price Types',7),(35,'2021-10-29 14:09:34.218817','2021-10-29 14:09:34.351596','role_permission_view','View Role Permissions',2),(36,'2021-10-29 14:09:34.218817','2021-10-29 14:09:34.351778','role_permissions_save','Save A Role\'s Permissions',2),(37,'2021-10-29 14:09:34.218817','2021-10-29 14:09:34.354462','role_delete','Delete Role',2),(38,'2021-10-29 14:09:34.218816','2021-10-29 14:09:34.354707','role_view','View Role',2),(39,'2021-10-29 14:09:34.218816','2021-10-29 14:09:34.354906','role_edit','Edit Role',2),(40,'2021-10-29 14:09:34.218816','2021-10-29 14:09:34.355089','role_add','Create Role',2),(41,'2021-10-29 14:09:34.218816','2021-10-29 14:09:34.355833','user_assign_role','Assign A User Role(s)',1),(42,'2021-10-29 14:09:34.218816','2021-10-29 14:09:34.356129','user_delete','Delete User',1),(43,'2021-10-29 14:09:34.218815','2021-10-29 14:09:34.356331','user_view','View User',1),(44,'2021-10-29 14:09:34.218814','2021-10-29 14:09:34.356512','user_edit_any','Edit Any User',1),(45,'2021-10-29 14:09:34.218814','2021-10-29 14:09:34.356689','user_edit','Edit User',1),(46,'2021-10-29 14:09:34.218814','2021-10-29 14:09:34.357526','user_add_any','Create Any User',1),(47,'2021-10-29 14:09:34.218751','2021-10-29 14:09:34.357727','user_add','Create Own Institution\'s User',1),(48,'2021-10-29 14:09:34.218817','2021-10-29 14:09:34.358411','product_category_view','View Product Categories',4),(49,'2021-10-29 14:09:34.218818','2021-10-29 14:09:34.358817','product_category_edit','Edit Product Categories',4),(50,'2021-10-29 14:09:34.218818','2021-10-29 14:09:34.359337','product_category_delete','Delete Product Categories',4),(51,'2021-10-29 14:09:34.218818','2021-10-29 14:09:34.359733','product_type_add','Add Product Types',5),(52,'2021-10-29 14:09:34.218818','2021-10-29 14:09:34.360300','product_type_view','View Product Types',5),(53,'2021-10-29 14:09:34.218819','2021-10-29 14:09:34.360811','product_type_edit','Edit Product Types',5),(54,'2021-10-29 14:09:34.218819','2021-10-29 14:09:34.361277','product_type_delete','Delete Product Types',5),(55,'2021-10-29 14:09:34.218819','2021-10-29 14:09:34.361658','attribute_add','Add Attributes',6),(56,'2021-10-29 14:09:34.218819','2021-10-29 14:09:34.362165','attribute_view','View Attributes',6),(57,'2021-10-29 14:09:34.218819','2021-10-29 14:09:34.362621','attribute_edit','Edit Attributes',6),(58,'2021-10-29 14:09:34.218820','2021-10-29 14:09:34.363003','attribute_delete','Delete Attributes',6),(59,'2021-10-29 14:09:34.218820','2021-10-29 14:09:34.363515','attribute_option_add','Add Attribute options',6),(60,'2021-10-29 14:09:34.218820','2021-10-29 14:09:34.364052','attribute_option_view','View Attribute options',6),(61,'2021-10-29 14:09:34.218820','2021-10-29 14:09:34.364598','attribute_option_edit','Edit Attribute options',6),(62,'2021-10-29 14:09:34.218820','2021-10-29 14:09:34.365099','attribute_option_delete','Delete Attribute options',6),(63,'2021-10-29 14:09:34.218823','2021-10-29 14:09:34.365560','price_type_add','Add Price Types',7),(64,'2021-10-29 14:09:34.218823','2021-10-29 14:09:34.365999','price_type_edit','Edit Price Types',7),(65,'2021-10-29 14:09:34.218830','2021-10-29 14:09:34.366505','most_selling_and_profitable_products_view','View Most Selling & Most Profitable Products',13);
/*!40000 ALTER TABLE `permissions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `price_types`
--

DROP TABLE IF EXISTS `price_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `price_types` (
                               `id` smallint unsigned NOT NULL AUTO_INCREMENT,
                               `tenant_id` char(36) NOT NULL,
                               `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                               `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                               `name` longtext NOT NULL,
                               PRIMARY KEY (`id`),
                               KEY `IX_price_types_tenant_id` (`tenant_id`),
                               CONSTRAINT `FK_price_types_tenants_tenant_id` FOREIGN KEY (`tenant_id`) REFERENCES `tenants` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `price_types`
--

LOCK TABLES `price_types` WRITE;
/*!40000 ALTER TABLE `price_types` DISABLE KEYS */;
/*!40000 ALTER TABLE `price_types` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_attribute`
--

DROP TABLE IF EXISTS `product_attribute`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product_attribute` (
                                     `attribute_id` smallint unsigned NOT NULL,
                                     `product_id` int unsigned NOT NULL,
                                     `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                                     `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                                     PRIMARY KEY (`product_id`,`attribute_id`),
                                     KEY `IX_product_attribute_attribute_id` (`attribute_id`),
                                     CONSTRAINT `FK_product_attribute_attributes_attribute_id` FOREIGN KEY (`attribute_id`) REFERENCES `attributes` (`id`) ON DELETE CASCADE,
                                     CONSTRAINT `FK_product_attribute_products_product_id` FOREIGN KEY (`product_id`) REFERENCES `products` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_attribute`
--

LOCK TABLES `product_attribute` WRITE;
/*!40000 ALTER TABLE `product_attribute` DISABLE KEYS */;
/*!40000 ALTER TABLE `product_attribute` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_attribute_options`
--

DROP TABLE IF EXISTS `product_attribute_options`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product_attribute_options` (
                                             `attribute_option_id` smallint unsigned NOT NULL,
                                             `product_id` int unsigned NOT NULL,
                                             `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                                             `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                                             PRIMARY KEY (`product_id`,`attribute_option_id`),
                                             KEY `IX_product_attribute_options_attribute_option_id` (`attribute_option_id`),
                                             CONSTRAINT `FK_product_attribute_options_attribute_options_attribute_option~` FOREIGN KEY (`attribute_option_id`) REFERENCES `attribute_options` (`id`) ON DELETE CASCADE,
                                             CONSTRAINT `FK_product_attribute_options_products_product_id` FOREIGN KEY (`product_id`) REFERENCES `products` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_attribute_options`
--

LOCK TABLES `product_attribute_options` WRITE;
/*!40000 ALTER TABLE `product_attribute_options` DISABLE KEYS */;
/*!40000 ALTER TABLE `product_attribute_options` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_categories`
--

DROP TABLE IF EXISTS `product_categories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product_categories` (
                                      `id` smallint unsigned NOT NULL AUTO_INCREMENT,
                                      `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                                      `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                                      `name` varchar(255) NOT NULL,
                                      `product_group_id` smallint unsigned NOT NULL,
                                      `parent_category_id` smallint unsigned DEFAULT NULL,
                                      PRIMARY KEY (`id`),
                                      UNIQUE KEY `IX_product_categories_name` (`name`),
                                      KEY `IX_product_categories_parent_category_id` (`parent_category_id`),
                                      KEY `IX_product_categories_product_group_id` (`product_group_id`),
                                      CONSTRAINT `FK_product_categories_product_categories_parent_category_id` FOREIGN KEY (`parent_category_id`) REFERENCES `product_categories` (`id`) ON DELETE RESTRICT,
                                      CONSTRAINT `FK_product_categories_product_groups_product_group_id` FOREIGN KEY (`product_group_id`) REFERENCES `product_groups` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=9;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_categories`
--

LOCK TABLES `product_categories` WRITE;
/*!40000 ALTER TABLE `product_categories` DISABLE KEYS */;
INSERT INTO `product_categories` VALUES (1,'2021-11-11 16:23:05.730788','2021-11-11 16:23:05.749188','GRAINS & CEREALS',8,NULL),(2,'2021-11-11 16:28:05.933234','2021-11-11 16:28:05.939727','MEAT, SEAFOOD & EGGS',8,NULL),(3,'2021-11-11 16:24:07.269927','2021-11-11 16:24:07.274565','FRUITS',8,NULL),(4,'2021-11-11 16:24:12.474696','2021-11-11 16:24:12.480086','PROCESSED PRODUCTS',8,NULL),(5,'2021-11-11 16:24:18.276754','2021-11-11 16:24:18.283434','DIARY',8,NULL),(6,'2021-11-11 16:24:22.849863','2021-11-11 16:24:22.855354','VEGETABLES',8,NULL),(7,'2021-11-11 16:24:27.196358','2021-11-11 16:24:27.201428','SPICES',8,NULL),(8,'2021-11-11 18:42:52.186721','2021-11-11 18:42:52.191511','Legumes',8,NULL);
/*!40000 ALTER TABLE `product_categories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_groups`
--

DROP TABLE IF EXISTS `product_groups`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product_groups` (
                                  `id` smallint unsigned NOT NULL AUTO_INCREMENT,
                                  `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                                  `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                                  `name` varchar(255) NOT NULL,
                                  PRIMARY KEY (`id`),
                                  UNIQUE KEY `IX_product_groups_name` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=9;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_groups`
--

LOCK TABLES `product_groups` WRITE;
/*!40000 ALTER TABLE `product_groups` DISABLE KEYS */;
INSERT INTO `product_groups` VALUES (7,'2021-11-08 11:21:48.207717','2021-11-08 11:21:48.212928','SOKONI PRODUCTS'),(8,'2021-11-11 16:22:30.599163','2021-11-11 16:22:30.601472','Grocery');
/*!40000 ALTER TABLE `product_groups` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_images`
--

DROP TABLE IF EXISTS `product_images`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product_images` (
                                  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
                                  `tenant_id` char(36) NOT NULL,
                                  `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                                  `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                                  `product_id` int unsigned NOT NULL,
                                  `image_path` longtext NOT NULL,
                                  PRIMARY KEY (`id`),
                                  KEY `IX_product_images_product_id` (`product_id`),
                                  KEY `IX_product_images_tenant_id` (`tenant_id`),
                                  CONSTRAINT `FK_product_images_products_product_id` FOREIGN KEY (`product_id`) REFERENCES `products` (`id`) ON DELETE CASCADE,
                                  CONSTRAINT `FK_product_images_tenants_tenant_id` FOREIGN KEY (`tenant_id`) REFERENCES `tenants` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_images`
--

LOCK TABLES `product_images` WRITE;
/*!40000 ALTER TABLE `product_images` DISABLE KEYS */;
/*!40000 ALTER TABLE `product_images` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_types`
--

DROP TABLE IF EXISTS `product_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product_types` (
                                 `id` smallint unsigned NOT NULL AUTO_INCREMENT,
                                 `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                                 `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                                 `name` longtext NOT NULL,
                                 `product_category_id` smallint unsigned NOT NULL,
                                 PRIMARY KEY (`id`),
                                 KEY `IX_product_types_product_category_id` (`product_category_id`),
                                 CONSTRAINT `FK_product_types_product_categories_product_category_id` FOREIGN KEY (`product_category_id`) REFERENCES `product_categories` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=165;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_types`
--

LOCK TABLES `product_types` WRITE;
/*!40000 ALTER TABLE `product_types` DISABLE KEYS */;
INSERT INTO `product_types` VALUES (1,'2021-11-08 11:28:53.590863','2021-11-08 11:28:53.611492','MILK',5),(2,'2021-11-08 11:29:26.767763','2021-11-08 11:29:26.774523','YOGHURT',5),(3,'2021-11-08 11:29:50.056975','2021-11-08 11:29:50.061645','CHEESE',5),(4,'2021-11-09 14:18:55.428862','2021-11-09 14:18:55.475480','MIXED BEEF',2),(5,'2021-11-09 14:19:24.997510','2021-11-09 14:19:25.002546','BROILER CHICKEN',2),(6,'2021-11-09 14:19:44.507356','2021-11-09 14:19:44.512350','LOCAL CHICKEN',2),(7,'2021-11-09 14:20:05.374596','2021-11-09 14:20:05.379510','PORK',2),(8,'2021-11-09 14:20:36.424273','2021-11-09 14:20:36.427944','FIRIGISI ',2),(9,'2021-11-09 14:20:55.117282','2021-11-09 14:20:55.120337','MAINI',2),(10,'2021-11-09 14:21:12.717975','2021-11-09 14:21:12.725775','EGGS',2),(11,'2021-11-09 14:21:31.888765','2021-11-09 14:21:31.892275','BEEF FILLET',2),(12,'2021-11-09 14:21:47.957572','2021-11-09 14:21:47.965454','BEEF STEAK',2),(13,'2021-11-09 14:22:03.950008','2021-11-09 14:22:03.958432','UTUMBO',2),(14,'2021-11-09 14:23:28.273400','2021-11-09 14:23:28.278046','MBUZI',2),(15,'2021-11-09 14:23:52.006291','2021-11-09 14:23:52.011063','MINCED MEAT',2),(16,'2021-11-09 14:25:10.776778','2021-11-09 14:25:10.782492','SATO',2),(17,'2021-11-09 14:25:29.677326','2021-11-09 14:25:29.680906','DAGAA',2),(18,'2021-11-09 14:25:47.623320','2021-11-09 14:25:47.629173','SANGARA',2),(19,'2021-11-09 14:26:08.022441','2021-11-09 14:26:08.027526','KONGORO',2),(20,'2021-11-09 14:26:26.736995','2021-11-09 14:26:26.740137','ULIMI',2),(21,'2021-11-09 14:27:26.154794','2021-11-09 14:27:26.158219','MCHELE',1),(22,'2021-11-09 14:27:51.433927','2021-11-09 14:27:51.437618','BASMAT RICE',1),(23,'2021-11-09 14:28:17.181277','2021-11-09 14:28:17.183861','SEMBE',1),(24,'2021-11-09 14:28:39.325363','2021-11-09 14:28:39.329431','UNGA MUHOGO',1),(25,'2021-11-09 14:28:57.463209','2021-11-09 14:28:57.465503','MUHOGO',1),(26,'2021-11-09 14:29:13.754272','2021-11-09 14:29:13.758889','DONA',1),(27,'2021-11-09 14:29:44.879259','2021-11-09 14:29:44.883080','NGANO PPF',1),(28,'2021-11-09 14:30:01.577107','2021-11-09 14:30:01.581254','NGANO HBF',1),(29,'2021-11-09 14:31:15.059842','2021-11-09 14:31:15.065210','MAHARAGE MABICHI',1),(30,'2021-11-09 14:31:43.141180','2021-11-09 14:31:43.146328','MAHARAGE NJANO',1),(31,'2021-11-09 14:32:05.596856','2021-11-09 14:32:05.599319','MAHARAGE SOYA',1),(32,'2021-11-09 14:34:29.165684','2021-11-09 14:34:29.168958','MAHARAGE SOYA',1),(33,'2021-11-09 14:34:52.574575','2021-11-09 14:34:52.578443','NJEGERE',1),(34,'2021-11-09 14:35:15.322051','2021-11-09 14:35:15.326364','GREEN BEANS',1),(35,'2021-11-09 14:35:32.966118','2021-11-09 14:35:32.969103','CHOROKO',1),(36,'2021-11-09 14:36:10.601000','2021-11-09 14:36:10.606004','MTAMA',1),(37,'2021-11-09 14:36:24.557488','2021-11-09 14:36:24.563821','ULEZI',1),(38,'2021-11-09 14:37:15.365876','2021-11-09 14:37:15.369902','NJUGU MAWE',1),(39,'2021-11-09 14:37:29.900951','2021-11-09 14:37:29.903754','UBUYU',1),(40,'2021-11-09 14:37:46.208821','2021-11-09 14:37:46.211588','SAMARIA',1),(41,'2021-11-09 14:39:28.489285','2021-11-09 14:39:28.492398','NYANYA',6),(42,'2021-11-09 14:39:52.751668','2021-11-09 14:39:52.755253','VITUNGUU MAJI',6),(43,'2021-11-09 14:40:13.874640','2021-11-09 14:40:13.878294','VIAZI',6),(44,'2021-11-09 14:40:37.514766','2021-11-09 14:40:37.518354','CARROT',6),(45,'2021-11-09 14:41:02.794548','2021-11-09 14:41:02.797898','HOHO KIJANI',6),(46,'2021-11-09 14:41:22.993761','2021-11-09 14:41:22.998218','HOHO ZA RANGI',6),(47,'2021-11-09 14:41:48.385855','2021-11-09 14:41:48.389721','MATANGO',6),(48,'2021-11-09 14:42:08.293327','2021-11-09 14:42:08.296496','CABBAGE',6),(49,'2021-11-09 14:42:30.826270','2021-11-09 14:42:30.829624','RED CABBAGE',6),(50,'2021-11-09 14:42:50.363160','2021-11-09 14:42:50.366795','TANGAWIZI',6),(51,'2021-11-09 14:43:20.515180','2021-11-09 14:43:20.520797','PILIPILI MWENDOKASI',6),(52,'2021-11-09 14:45:10.088608','2021-11-09 14:45:10.090888','VITUNGUU SWAUMU KICHINA',6),(53,'2021-11-09 14:45:44.309512','2021-11-09 14:45:44.312576','VITUNGUU SWAUMU LOCAL',6),(54,'2021-11-09 14:46:08.982360','2021-11-09 14:46:08.985954','LIMAO',6),(55,'2021-11-09 14:46:38.921194','2021-11-09 14:46:38.923537','NDIZI BUKOBA',6),(56,'2021-11-09 14:47:00.870007','2021-11-09 14:47:00.873996','NDIZI MZUZU',6),(57,'2021-11-10 08:56:55.213551','2021-11-10 08:56:55.217969','NDIZI MALINDI',6),(58,'2021-11-10 08:58:40.557679','2021-11-10 08:58:40.560154','NDIZI MSHALE',6),(59,'2021-11-10 08:58:59.691474','2021-11-10 08:58:59.695277','VIAZI LISHE',6),(60,'2021-11-10 08:59:15.337194','2021-11-10 08:59:15.340655','VIAZI VITAMU',6),(61,'2021-11-10 09:00:00.698895','2021-11-10 09:00:00.704041','NYANYA CHUNGU',6),(62,'2021-11-10 09:00:20.185570','2021-11-10 09:00:20.189385','BIRINGANYA',6),(63,'2021-11-10 09:00:43.766741','2021-11-10 09:00:43.770077','NAZI TUNDA',6),(64,'2021-11-10 09:01:00.849645','2021-11-10 09:01:00.852938','MBOGA ZA MAJANI',6),(65,'2021-11-10 09:01:15.295743','2021-11-10 09:01:15.299118','LEEKS',6),(66,'2021-11-10 09:01:29.262914','2021-11-10 09:01:29.269627','CERELY',6),(67,'2021-11-10 09:01:44.653593','2021-11-10 09:01:44.657009','LETTUCE',6),(68,'2021-11-10 09:02:03.420437','2021-11-10 09:02:03.422607','CAULIFLOWER',6),(69,'2021-11-10 09:02:23.726274','2021-11-10 09:02:23.730036','BROCOLLI',6),(70,'2021-11-10 09:02:42.786423','2021-11-10 09:02:42.790597','ZUCCHINI',6),(71,'2021-11-10 09:03:01.934637','2021-11-10 09:03:01.938222','BUTTERNUT',6),(72,'2021-11-10 09:03:26.504560','2021-11-10 09:03:26.507823','VITUNGUU MAJANI',6),(73,'2021-11-10 09:03:50.978786','2021-11-10 09:03:50.982113','GILIGILANI',6),(74,'2021-11-10 09:04:07.517799','2021-11-10 09:04:07.520720','MAGIMBI',6),(75,'2021-11-10 09:04:49.099160','2021-11-10 09:04:49.102785','PAPAI',3),(76,'2021-11-10 09:05:01.412500','2021-11-10 09:05:01.415563','NANASI',3),(77,'2021-11-10 09:05:15.846466','2021-11-10 09:05:15.849948','TIKITI',3),(78,'2021-11-10 09:05:30.269981','2021-11-10 09:05:30.274671','PASSION',3),(79,'2021-11-10 09:07:01.497554','2021-11-10 09:07:01.501347','PARACHICHI',3),(80,'2021-11-10 09:07:18.378950','2021-11-10 09:07:18.382310','MANGO',3),(81,'2021-11-10 09:07:36.097586','2021-11-10 09:07:36.100855','BEETROOT',3),(82,'2021-11-10 09:07:54.057327','2021-11-10 09:07:54.060167','ORANGE',3),(83,'2021-11-10 09:08:08.669426','2021-11-10 09:08:08.672378','CHOYA',3),(84,'2021-11-10 09:08:25.529555','2021-11-10 09:08:25.532429','ZABIBU',3),(85,'2021-11-10 09:08:54.156863','2021-11-10 09:08:54.160394','NDIZI MBIVU',3),(86,'2021-11-10 09:09:33.050857','2021-11-10 09:09:33.054848','BIRIYAN MASALA',7),(87,'2021-11-10 09:09:51.496967','2021-11-10 09:09:51.500008','PILAU MASALA',7),(88,'2021-11-10 09:10:13.066171','2021-11-10 09:10:13.071237','TEA MASALA',7),(89,'2021-11-10 09:10:34.790333','2021-11-10 09:10:34.794110','CUMMIN POWDER',7),(90,'2021-11-10 09:11:01.113407','2021-11-10 09:11:01.116988','CINNAMON POWDER',7),(91,'2021-11-10 09:11:27.146348','2021-11-10 09:11:27.149075','CARDAMON POWDER',7),(92,'2021-11-10 09:11:57.741516','2021-11-10 09:11:57.745015','BLACK PEPPER',7),(93,'2021-11-10 09:12:14.322638','2021-11-10 09:12:14.326568','FISH MASALA',7),(94,'2021-11-10 09:12:32.359585','2021-11-10 09:12:32.363851','BEEF MASALA',7),(95,'2021-11-10 09:12:55.153233','2021-11-10 09:12:55.156065','CHICKEN MASALA',7),(96,'2021-11-10 09:42:53.548668','2021-11-10 09:42:53.577397','TOMATO PASTE',4),(97,'2021-11-10 09:43:12.550581','2021-11-10 09:43:12.557929','MUSHROOM',4),(98,'2021-11-10 09:43:41.989559','2021-11-10 09:43:41.995938','BAKING POWDER',4),(99,'2021-11-10 09:15:22.552872','2021-11-10 09:15:22.554634','SOY SAUCE',4),(100,'2021-11-10 09:15:41.287675','2021-11-10 09:15:41.291811','MAMA SITA',4),(101,'2021-11-10 09:15:59.263542','2021-11-10 09:15:59.265599','VINEGAR',4),(102,'2021-11-10 09:32:31.974003','2021-11-10 09:32:31.977893','VANILLA',4),(103,'2021-11-10 09:32:54.473729','2021-11-10 09:32:54.477636','NUTELLA',4),(104,'2021-11-10 09:33:18.085454','2021-11-10 09:33:18.089336','AJNA MOTO',4),(105,'2021-11-10 09:33:35.433827','2021-11-10 09:33:35.437735','SUKARI',4),(106,'2021-11-10 09:33:51.477805','2021-11-10 09:33:51.482197','KETCHUP',4),(107,'2021-11-10 09:34:16.193911','2021-11-10 09:34:16.198161','MAFUTA SAFI',4),(108,'2021-11-10 09:34:37.273903','2021-11-10 09:34:37.277336','MAFUTA ALIZETI',4),(109,'2021-11-10 09:34:52.365705','2021-11-10 09:34:52.368868','SUNDROP',4),(110,'2021-11-10 09:35:12.677592','2021-11-10 09:35:12.681911','BLUE BAND',4),(111,'2021-11-10 09:35:37.215428','2021-11-10 09:35:37.219289','PRESTIGE',4),(112,'2021-11-10 09:36:00.425460','2021-11-10 09:36:00.428781','JAM',4),(113,'2021-11-10 09:36:53.597107','2021-11-10 09:36:53.600696','TOMATO SAUCE',4),(114,'2021-11-10 09:37:11.637777','2021-11-10 09:37:11.640969','AMIRA',4),(115,'2021-11-10 09:37:26.941454','2021-11-10 09:37:26.945104','CHUMVI',4),(116,'2021-11-10 09:38:16.071389','2021-11-10 09:38:16.076169','AZAM NAZI',4),(117,'2021-11-10 09:38:38.318066','2021-11-10 09:38:38.321342','SIMBA NAZI',4),(118,'2021-11-10 09:38:57.649871','2021-11-10 09:38:57.653797','MAYONNISE',4),(119,'2021-11-10 09:39:21.069196','2021-11-10 09:39:21.073815','ROYCO',4),(120,'2021-11-10 09:39:40.889630','2021-11-10 09:39:40.893053','CURRY POWDER',4),(121,'2021-11-10 09:39:56.738412','2021-11-10 09:39:56.743480','AROMAT',4),(122,'2021-11-10 09:40:14.826703','2021-11-10 09:40:14.829701','FOOD COLOR',4),(123,'2021-11-10 09:40:32.034194','2021-11-10 09:40:32.040693','MTINDI',4),(124,'2021-11-10 09:40:53.549402','2021-11-10 09:40:53.552495','MAJANI YA CHAI',4),(125,'2021-11-10 09:41:14.273883','2021-11-10 09:41:14.275779','CORN FLOUR',4),(126,'2021-11-10 09:41:34.713314','2021-11-10 09:41:34.717497','NDIMU YA UNGA',4),(127,'2021-11-10 09:44:53.758703','2021-11-10 09:44:53.763014','IRIKI',7),(128,'2021-11-10 09:45:21.829215','2021-11-10 09:45:21.832580','BINZARI NYEMBAMBA',7),(129,'2021-11-10 09:45:40.144871','2021-11-10 09:45:40.148065','KARAFUU',7),(130,'2021-11-10 09:45:59.504991','2021-11-10 09:45:59.508519','MDALASINI',7),(131,'2021-11-10 09:46:17.757809','2021-11-10 09:46:17.761268','PILIPILI MANGA',7),(132,'2021-11-10 09:46:47.023416','2021-11-10 09:46:47.026292','TISSUE',4),(133,'2021-11-10 09:47:08.858741','2021-11-10 09:47:08.861841','FOIL',4),(134,'2021-11-10 09:47:24.737673','2021-11-10 09:47:24.741963','CLING FILM',4),(135,'2021-11-10 09:47:40.574813','2021-11-10 09:47:40.578356','BAHASHA',4),(136,'2021-11-10 09:47:57.673956','2021-11-10 09:47:57.677962','LUNCH BOX',4),(137,'2021-11-10 09:48:17.201593','2021-11-10 09:48:17.205402','TOOTH PICK',4),(138,'2021-11-10 09:57:02.433741','2021-11-10 09:57:02.547993','BAMBOO STICK',4),(139,'2021-11-10 09:57:20.846454','2021-11-10 09:57:20.849989','SABUNI',4),(140,'2021-11-10 09:57:40.666039','2021-11-10 09:57:40.669059','TARMOL',4),(141,'2021-11-10 09:57:57.003273','2021-11-10 09:57:57.006517','STEEL WIRE',4),(142,'2021-11-10 09:58:20.314203','2021-11-10 09:58:20.317957','SPAGHETTI',4),(143,'2021-11-10 09:58:46.222456','2021-11-10 09:58:46.225170','KIBERITI',4),(144,'2021-11-10 09:59:11.761363','2021-11-10 09:59:11.764029','BURGER BUNS',4),(145,'2021-11-11 16:29:54.821299','2021-11-11 16:29:54.823699','Apples and Pears',3),(146,'2021-11-11 16:30:19.386910','2021-11-11 16:30:19.390681','Citrus',3),(147,'2021-11-11 16:30:41.705538','2021-11-11 16:30:41.708791','Stone fruit',3),(149,'2021-11-11 18:36:56.043814','2021-11-11 18:36:56.054353','Tropical',3),(150,'2021-11-11 18:37:10.564747','2021-11-11 18:37:10.567872','Berries',3),(151,'2021-11-11 18:37:22.783506','2021-11-11 18:37:22.786643','Melons',3),(152,'2021-11-11 18:37:41.693485','2021-11-11 18:37:41.696716','Tomatoes and avocados',3),(153,'2021-11-11 18:38:32.998824','2021-11-11 18:38:33.002045','Grains',1),(154,'2021-11-11 18:38:43.311813','2021-11-11 18:38:43.315841','Cereals',1),(155,'2021-11-11 18:40:26.697064','2021-11-11 18:40:26.700608','Leafy green',6),(156,'2021-11-11 18:40:41.329966','2021-11-11 18:40:41.333722','Cruciferous',6),(157,'2021-11-11 18:40:54.665428','2021-11-11 18:40:54.667480','Marrow',6),(158,'2021-11-11 18:41:12.914609','2021-11-11 18:41:12.919961','Roots',6),(159,'2021-11-11 18:41:26.533393','2021-11-11 18:41:26.535790','Edible plant stem',6),(160,'2021-11-11 18:41:38.663864','2021-11-11 18:41:38.666860','Allium',6),(161,'2021-11-11 18:44:20.269287','2021-11-11 18:44:20.272679','Soy products',8),(162,'2021-11-11 18:44:35.783258','2021-11-11 18:44:35.786928','Legume flours',8),(164,'2021-11-11 18:45:16.022273','2021-11-11 18:45:16.026047','Beans',8);
/*!40000 ALTER TABLE `product_types` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `products`
--

DROP TABLE IF EXISTS `products`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `products` (
                            `id` int unsigned NOT NULL AUTO_INCREMENT,
                            `tenant_id` char(36) NOT NULL,
                            `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                            `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                            `name` longtext NOT NULL,
                            `image_path` longtext,
                            `slug` longtext,
                            `product_type_id` smallint unsigned NOT NULL,
                            `has_expiration` tinyint(1) NOT NULL,
                            PRIMARY KEY (`id`),
                            KEY `IX_products_product_type_id` (`product_type_id`),
                            KEY `IX_products_tenant_id` (`tenant_id`),
                            CONSTRAINT `FK_products_product_types_product_type_id` FOREIGN KEY (`product_type_id`) REFERENCES `product_types` (`id`) ON DELETE CASCADE,
                            CONSTRAINT `FK_products_tenants_tenant_id` FOREIGN KEY (`tenant_id`) REFERENCES `tenants` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products`
--

LOCK TABLES `products` WRITE;
/*!40000 ALTER TABLE `products` DISABLE KEYS */;
/*!40000 ALTER TABLE `products` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role_claims`
--

DROP TABLE IF EXISTS `role_claims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `role_claims` (
                               `id` int NOT NULL AUTO_INCREMENT,
                               `role_id` bigint NOT NULL,
                               `claim_type` longtext,
                               `claim_value` varchar(255) NOT NULL,
                               PRIMARY KEY (`id`),
                               UNIQUE KEY `AK_role_claims_role_id_claim_value` (`role_id`,`claim_value`),
                               CONSTRAINT `FK_role_claims_roles_role_id` FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=941;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role_claims`
--

LOCK TABLES `role_claims` WRITE;
/*!40000 ALTER TABLE `role_claims` DISABLE KEYS */;
INSERT INTO `role_claims` VALUES (1,1,'permission','most_selling_and_profitable_products_view'),(2,2,'permission','stock_and_expiration_summary_view'),(3,2,'permission','summary_view'),(4,2,'permission','business_info_update'),(5,2,'permission','business_info_view'),(6,2,'permission','sale_invoice_change_date'),(7,2,'permission','sale_invoice_view'),(8,2,'permission','sale_view'),(9,2,'permission','sale_record'),(10,2,'permission','sale_edit'),(11,2,'permission','sale_sell'),(12,2,'permission','stock_delete'),(13,2,'permission','stock_edit'),(14,2,'permission','stock_view'),(15,2,'permission','product_group_add'),(16,2,'permission','stock_add'),(17,2,'permission','product_edit'),(18,2,'permission','product_view'),(19,2,'permission','product_add'),(20,2,'permission','expenditure_delete'),(21,2,'permission','expenditure_edit'),(22,2,'permission','expenditure_view'),(23,2,'permission','expenditure_add'),(24,2,'permission','expenditure_type_delete'),(25,2,'permission','expenditure_type_edit'),(26,2,'permission','expenditure_type_view'),(27,2,'permission','expenditure_type_add'),(28,2,'permission','product_group_delete'),(29,2,'permission','product_group_edit'),(30,2,'permission','product_delete'),(31,2,'permission','sales_graph'),(32,2,'permission','price_type_delete'),(33,2,'permission','price_type_view'),(34,2,'permission','price_type_add'),(35,2,'permission','attribute_option_delete'),(36,2,'permission','attribute_option_edit'),(37,2,'permission','attribute_option_view'),(38,2,'permission','attribute_option_add'),(39,2,'permission','attribute_delete'),(40,2,'permission','attribute_edit'),(41,2,'permission','attribute_view'),(42,2,'permission','attribute_add'),(43,2,'permission','product_type_delete'),(44,2,'permission','product_type_edit'),(45,2,'permission','product_type_view'),(46,2,'permission','product_type_add'),(47,2,'permission','product_category_delete'),(48,2,'permission','product_category_edit'),(49,2,'permission','product_category_view'),(50,2,'permission','user_add'),(51,2,'permission','user_add_any'),(52,2,'permission','user_edit'),(53,2,'permission','user_edit_any'),(54,2,'permission','user_view'),(55,2,'permission','user_delete'),(56,2,'permission','user_assign_role'),(57,2,'permission','role_add'),(58,2,'permission','role_edit'),(59,2,'permission','role_view'),(60,2,'permission','role_delete'),(61,2,'permission','role_permissions_save'),(62,2,'permission','role_permission_view'),(63,2,'permission','product_group_view'),(64,2,'permission','product_category_add'),(65,1,'permission','product_category_add'),(66,1,'permission','product_group_view'),(67,1,'permission','role_permissions_save'),(68,1,'permission','role_delete'),(69,1,'permission','role_view'),(70,1,'permission','role_edit'),(71,1,'permission','role_add'),(72,1,'permission','user_assign_role'),(73,1,'permission','user_delete'),(74,1,'permission','user_view'),(75,1,'permission','user_edit_any'),(76,1,'permission','user_edit'),(77,1,'permission','user_add_any'),(78,1,'permission','user_add'),(79,1,'permission','product_category_view'),(80,1,'permission','product_category_edit'),(81,1,'permission','product_category_delete'),(82,1,'permission','product_type_add'),(83,1,'permission','product_type_view'),(84,1,'permission','product_type_edit'),(85,1,'permission','product_type_delete'),(86,1,'permission','attribute_add'),(87,1,'permission','attribute_view'),(88,1,'permission','attribute_edit'),(89,1,'permission','attribute_delete'),(90,1,'permission','attribute_option_add'),(91,1,'permission','attribute_option_view'),(92,1,'permission','attribute_option_edit'),(93,1,'permission','attribute_option_delete'),(94,1,'permission','price_type_add'),(95,1,'permission','price_type_edit'),(96,1,'permission','role_permission_view'),(97,2,'permission','price_type_edit'),(98,1,'permission','price_type_view'),(99,1,'permission','sales_graph'),(100,1,'permission','product_group_edit'),(101,1,'permission','product_group_delete'),(102,1,'permission','expenditure_type_add'),(103,1,'permission','expenditure_type_view'),(104,1,'permission','expenditure_type_edit'),(105,1,'permission','expenditure_type_delete'),(106,1,'permission','expenditure_add'),(107,1,'permission','expenditure_view'),(108,1,'permission','expenditure_edit'),(109,1,'permission','expenditure_delete'),(110,1,'permission','product_add'),(111,1,'permission','product_view'),(112,1,'permission','product_edit'),(113,1,'permission','product_delete'),(114,1,'permission','stock_add'),(115,1,'permission','stock_view'),(116,1,'permission','stock_edit'),(117,1,'permission','stock_delete'),(118,1,'permission','sale_sell'),(119,1,'permission','sale_edit'),(120,1,'permission','sale_record'),(121,1,'permission','sale_view'),(122,1,'permission','sale_invoice_view'),(123,1,'permission','sale_invoice_change_date'),(124,1,'permission','business_info_view'),(125,1,'permission','business_info_update'),(126,1,'permission','summary_view'),(127,1,'permission','stock_and_expiration_summary_view'),(128,1,'permission','product_group_add'),(129,1,'permission','price_type_delete'),(130,2,'permission','most_selling_and_profitable_products_view'),(781,13,'permission','product_category_add'),(782,13,'permission','role_permissions_save'),(783,13,'permission','role_delete'),(784,13,'permission','role_view'),(785,13,'permission','role_edit'),(786,13,'permission','role_add'),(787,13,'permission','user_assign_role'),(788,13,'permission','user_delete'),(789,13,'permission','user_view'),(790,13,'permission','user_edit_any'),(791,13,'permission','user_edit'),(792,13,'permission','user_add_any'),(793,13,'permission','user_add'),(794,13,'permission','product_category_view'),(795,13,'permission','product_category_edit'),(796,13,'permission','product_category_delete'),(797,13,'permission','product_type_add'),(798,13,'permission','product_type_view'),(799,13,'permission','product_type_edit'),(800,13,'permission','product_type_delete'),(801,13,'permission','attribute_add'),(802,13,'permission','attribute_view'),(803,13,'permission','attribute_edit'),(804,13,'permission','attribute_delete'),(805,13,'permission','attribute_option_add'),(806,13,'permission','attribute_option_view'),(807,13,'permission','attribute_option_edit'),(808,13,'permission','attribute_option_delete'),(809,13,'permission','price_type_add'),(810,13,'permission','price_type_edit'),(811,13,'permission','role_permission_view'),(812,13,'permission','price_type_view'),(813,13,'permission','price_type_delete'),(814,13,'permission','sales_graph'),(815,13,'permission','product_group_view'),(816,13,'permission','product_group_edit'),(817,13,'permission','product_group_delete'),(818,13,'permission','expenditure_type_add'),(819,13,'permission','expenditure_type_view'),(820,13,'permission','expenditure_type_edit'),(821,13,'permission','expenditure_type_delete'),(822,13,'permission','expenditure_add'),(823,13,'permission','expenditure_view'),(824,13,'permission','expenditure_edit'),(825,13,'permission','expenditure_delete'),(826,13,'permission','product_add'),(827,13,'permission','product_view'),(828,13,'permission','product_edit'),(829,13,'permission','most_selling_and_profitable_products_view'),(830,13,'permission','product_delete'),(831,13,'permission','stock_view'),(832,13,'permission','stock_edit'),(833,13,'permission','stock_delete'),(834,13,'permission','sale_sell'),(835,13,'permission','sale_edit'),(836,13,'permission','sale_record'),(837,13,'permission','sale_view'),(838,13,'permission','sale_invoice_view'),(839,13,'permission','sale_invoice_change_date'),(840,13,'permission','business_info_view'),(841,13,'permission','business_info_update'),(842,13,'permission','summary_view'),(843,13,'permission','stock_and_expiration_summary_view'),(844,13,'permission','product_group_add'),(845,13,'permission','stock_add'),(846,14,'permission','product_category_add'),(847,14,'permission','role_permissions_save'),(848,14,'permission','role_delete'),(849,14,'permission','role_view'),(850,14,'permission','role_edit'),(851,14,'permission','role_add'),(852,14,'permission','user_assign_role'),(853,14,'permission','user_delete'),(854,14,'permission','user_view'),(855,14,'permission','user_edit_any'),(856,14,'permission','user_edit'),(857,14,'permission','user_add_any'),(858,14,'permission','user_add'),(859,14,'permission','product_category_view'),(860,14,'permission','product_category_edit'),(861,14,'permission','product_category_delete'),(862,14,'permission','product_type_add'),(863,14,'permission','product_type_view'),(864,14,'permission','product_type_edit'),(865,14,'permission','product_type_delete'),(866,14,'permission','attribute_add'),(867,14,'permission','attribute_view'),(868,14,'permission','attribute_edit'),(869,14,'permission','attribute_delete'),(870,14,'permission','attribute_option_add'),(871,14,'permission','attribute_option_view'),(872,14,'permission','attribute_option_edit'),(873,14,'permission','attribute_option_delete'),(874,14,'permission','price_type_add'),(875,14,'permission','price_type_edit'),(876,14,'permission','role_permission_view'),(877,14,'permission','price_type_view'),(878,14,'permission','price_type_delete'),(879,14,'permission','sales_graph'),(880,14,'permission','product_group_view'),(881,14,'permission','product_group_edit'),(882,14,'permission','product_group_delete'),(883,14,'permission','expenditure_type_add'),(884,14,'permission','expenditure_type_view'),(885,14,'permission','expenditure_type_edit'),(886,14,'permission','expenditure_type_delete'),(887,14,'permission','expenditure_add'),(888,14,'permission','expenditure_view'),(889,14,'permission','expenditure_edit'),(890,14,'permission','expenditure_delete'),(891,14,'permission','product_add'),(892,14,'permission','product_view'),(893,14,'permission','product_edit'),(894,14,'permission','most_selling_and_profitable_products_view'),(895,14,'permission','product_delete'),(896,14,'permission','stock_view'),(897,14,'permission','stock_edit'),(898,14,'permission','stock_delete'),(899,14,'permission','sale_sell'),(900,14,'permission','sale_edit'),(901,14,'permission','sale_record'),(902,14,'permission','sale_view'),(903,14,'permission','sale_invoice_view'),(904,14,'permission','sale_invoice_change_date'),(905,14,'permission','business_info_view'),(906,14,'permission','business_info_update'),(907,14,'permission','summary_view'),(908,14,'permission','stock_and_expiration_summary_view'),(909,14,'permission','product_group_add'),(910,14,'permission','stock_add'),(917,15,'permission','product_delete'),(918,15,'permission','product_edit'),(919,15,'permission','product_view'),(920,15,'permission','product_add'),(921,15,'permission','attribute_option_delete'),(922,15,'permission','attribute_option_edit'),(923,15,'permission','attribute_option_view'),(924,15,'permission','attribute_option_add'),(925,15,'permission','attribute_delete'),(926,15,'permission','attribute_edit'),(927,15,'permission','attribute_add'),(928,15,'permission','product_type_delete'),(929,15,'permission','product_type_edit'),(930,15,'permission','product_type_view'),(931,15,'permission','product_type_add'),(932,15,'permission','product_category_delete'),(933,15,'permission','product_category_edit'),(934,15,'permission','product_category_view'),(935,15,'permission','product_category_add'),(936,15,'permission','product_group_add'),(937,15,'permission','product_group_delete'),(938,15,'permission','product_group_edit'),(939,15,'permission','attribute_view'),(940,15,'permission','product_group_view');
/*!40000 ALTER TABLE `role_claims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `roles`
--

DROP TABLE IF EXISTS `roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `roles` (
                         `id` bigint NOT NULL AUTO_INCREMENT,
                         `tenant_id` char(36) NOT NULL,
                         `name` varchar(256) DEFAULT NULL,
                         `normalized_name` varchar(256) DEFAULT NULL,
                         `display_name` longtext NOT NULL,
                         `concurrency_stamp` longtext,
                         `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                         `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                         PRIMARY KEY (`id`),
                         UNIQUE KEY `RoleNameIndex` (`normalized_name`),
                         KEY `IX_roles_tenant_id` (`tenant_id`),
                         CONSTRAINT `FK_roles_tenants_tenant_id` FOREIGN KEY (`tenant_id`) REFERENCES `tenants` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=16;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `roles`
--

LOCK TABLES `roles` WRITE;
/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
INSERT INTO `roles` VALUES (1,'08d99ae5-bc22-4682-8c6c-8f3b888c45dd','Administrator','KEA_ADMINISTRATOR','Administrator',NULL,'2021-10-29 14:09:34.557134','2021-10-29 14:09:34.611523'),(2,'08d99ae5-bc26-46d2-88fa-470a234dca4c','Administrator','LOCALHOST_ADMINISTRATOR','Administrator',NULL,'2021-10-29 14:09:34.557234','2021-10-29 14:09:34.627539'),(13,'08d99b6c-c4e1-45e6-85a3-fa2cd7407e73','IJEN_OWNER','IJEN_OWNER','Owner',NULL,'2021-10-30 06:16:11.370128','2021-10-30 06:16:11.373148'),(14,'08d99b82-0aa5-4862-877c-71902a3dfccc','ZAKA_ADMIN','ZAKA_ADMIN','Admin',NULL,'2021-10-30 08:48:27.805595','2021-10-30 08:48:27.808715'),(15,'08d99b82-0aa5-4862-877c-71902a3dfccc','CLERK','CLERK','Clerk',NULL,'2021-11-11 12:50:57.654101','2021-11-11 12:50:57.665993');
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sale_invoices`
--

DROP TABLE IF EXISTS `sale_invoices`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sale_invoices` (
                                 `id` bigint unsigned NOT NULL AUTO_INCREMENT,
                                 `tenant_id` char(36) NOT NULL,
                                 `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                                 `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                                 `number` varchar(255) DEFAULT NULL,
                                 `amount` bigint unsigned NOT NULL,
                                 `date` datetime(6) NOT NULL,
                                 `is_completed` tinyint(1) NOT NULL,
                                 `is_canceled` tinyint(1) NOT NULL,
                                 `user_id` bigint NOT NULL,
                                 PRIMARY KEY (`id`),
                                 UNIQUE KEY `IX_sale_invoices_number` (`number`),
                                 KEY `IX_sale_invoices_tenant_id` (`tenant_id`),
                                 KEY `IX_sale_invoices_user_id` (`user_id`),
                                 CONSTRAINT `FK_sale_invoices_tenants_tenant_id` FOREIGN KEY (`tenant_id`) REFERENCES `tenants` (`id`) ON DELETE CASCADE,
                                 CONSTRAINT `FK_sale_invoices_users_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sale_invoices`
--

LOCK TABLES `sale_invoices` WRITE;
/*!40000 ALTER TABLE `sale_invoices` DISABLE KEYS */;
/*!40000 ALTER TABLE `sale_invoices` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sales`
--

DROP TABLE IF EXISTS `sales`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sales` (
                         `id` bigint unsigned NOT NULL AUTO_INCREMENT,
                         `tenant_id` char(36) NOT NULL,
                         `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                         `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                         `sku_id` bigint unsigned NOT NULL,
                         `sale_invoice_id` bigint unsigned NOT NULL,
                         `price` int unsigned NOT NULL,
                         `discount` int unsigned NOT NULL,
                         `profit` bigint NOT NULL,
                         `quantity` int NOT NULL,
                         `is_confirmed` tinyint(1) NOT NULL,
                         `product_id` int unsigned DEFAULT NULL,
                         PRIMARY KEY (`id`),
                         KEY `IX_sales_sale_invoice_id` (`sale_invoice_id`),
                         KEY `IX_sales_sku_id` (`sku_id`),
                         KEY `IX_sales_tenant_id` (`tenant_id`),
                         KEY `IX_sales_product_id` (`product_id`),
                         CONSTRAINT `FK_sales_products_product_id` FOREIGN KEY (`product_id`) REFERENCES `products` (`id`) ON DELETE RESTRICT,
                         CONSTRAINT `FK_sales_sale_invoices_sale_invoice_id` FOREIGN KEY (`sale_invoice_id`) REFERENCES `sale_invoices` (`id`) ON DELETE CASCADE,
                         CONSTRAINT `FK_sales_skus_sku_id` FOREIGN KEY (`sku_id`) REFERENCES `skus` (`id`) ON DELETE CASCADE,
                         CONSTRAINT `FK_sales_tenants_tenant_id` FOREIGN KEY (`tenant_id`) REFERENCES `tenants` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales`
--

LOCK TABLES `sales` WRITE;
/*!40000 ALTER TABLE `sales` DISABLE KEYS */;
/*!40000 ALTER TABLE `sales` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sku_attributes`
--

DROP TABLE IF EXISTS `sku_attributes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sku_attributes` (
                                  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
                                  `tenant_id` char(36) NOT NULL,
                                  `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                                  `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                                  `attribute_option_id` smallint unsigned NOT NULL,
                                  `stock_keeping_unit_id` bigint unsigned NOT NULL,
                                  PRIMARY KEY (`id`),
                                  KEY `IX_sku_attributes_attribute_option_id` (`attribute_option_id`),
                                  KEY `IX_sku_attributes_stock_keeping_unit_id` (`stock_keeping_unit_id`),
                                  KEY `IX_sku_attributes_tenant_id` (`tenant_id`),
                                  CONSTRAINT `FK_sku_attributes_attribute_options_attribute_option_id` FOREIGN KEY (`attribute_option_id`) REFERENCES `attribute_options` (`id`) ON DELETE CASCADE,
                                  CONSTRAINT `FK_sku_attributes_skus_stock_keeping_unit_id` FOREIGN KEY (`stock_keeping_unit_id`) REFERENCES `skus` (`id`) ON DELETE CASCADE,
                                  CONSTRAINT `FK_sku_attributes_tenants_tenant_id` FOREIGN KEY (`tenant_id`) REFERENCES `tenants` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sku_attributes`
--

LOCK TABLES `sku_attributes` WRITE;
/*!40000 ALTER TABLE `sku_attributes` DISABLE KEYS */;
/*!40000 ALTER TABLE `sku_attributes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sku_selling_prices`
--

DROP TABLE IF EXISTS `sku_selling_prices`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sku_selling_prices` (
                                      `sku_id` bigint unsigned NOT NULL,
                                      `price_type_id` smallint unsigned NOT NULL,
                                      `price` int unsigned DEFAULT NULL,
                                      `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                                      `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                                      PRIMARY KEY (`price_type_id`,`sku_id`),
                                      KEY `IX_sku_selling_prices_sku_id` (`sku_id`),
                                      CONSTRAINT `FK_sku_selling_prices_price_types_price_type_id` FOREIGN KEY (`price_type_id`) REFERENCES `price_types` (`id`) ON DELETE CASCADE,
                                      CONSTRAINT `FK_sku_selling_prices_skus_sku_id` FOREIGN KEY (`sku_id`) REFERENCES `skus` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sku_selling_prices`
--

LOCK TABLES `sku_selling_prices` WRITE;
/*!40000 ALTER TABLE `sku_selling_prices` DISABLE KEYS */;
/*!40000 ALTER TABLE `sku_selling_prices` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `skus`
--

DROP TABLE IF EXISTS `skus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `skus` (
                        `id` bigint unsigned NOT NULL AUTO_INCREMENT,
                        `tenant_id` char(36) NOT NULL,
                        `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                        `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                        `product_id` int unsigned NOT NULL,
                        `stock_date` datetime(6) NOT NULL,
                        `buying_price` int unsigned NOT NULL,
                        `selling_price` int unsigned NOT NULL,
                        `quantity` int NOT NULL,
                        `remaining_quantity` int NOT NULL,
                        `maximum_discount` int unsigned NOT NULL,
                        `low_stock_amount` int DEFAULT NULL,
                        `is_on_sale` tinyint(1) NOT NULL,
                        PRIMARY KEY (`id`),
                        KEY `IX_skus_product_id` (`product_id`),
                        KEY `IX_skus_tenant_id` (`tenant_id`),
                        CONSTRAINT `FK_skus_products_product_id` FOREIGN KEY (`product_id`) REFERENCES `products` (`id`) ON DELETE CASCADE,
                        CONSTRAINT `FK_skus_tenants_tenant_id` FOREIGN KEY (`tenant_id`) REFERENCES `tenants` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `skus`
--

LOCK TABLES `skus` WRITE;
/*!40000 ALTER TABLE `skus` DISABLE KEYS */;
/*!40000 ALTER TABLE `skus` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tenants`
--

DROP TABLE IF EXISTS `tenants`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tenants` (
                           `id` char(36) NOT NULL,
                           `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                           `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                           `name` varchar(255) DEFAULT NULL,
                           `code` varchar(255) DEFAULT NULL,
                           `email` longtext,
                           `address` longtext,
                           `phone_number_1` longtext,
                           `phone_number_2` longtext,
                           `description` longtext,
                           `domain` varchar(255) DEFAULT NULL,
                           `connection_string` longtext,
                           `logo_path` longtext,
                           `subscription_type` int NOT NULL,
                           `valid_to` datetime(6) NOT NULL,
                           `Active` tinyint(1) NOT NULL,
                           PRIMARY KEY (`id`),
                           UNIQUE KEY `IX_tenants_code` (`code`),
                           UNIQUE KEY `IX_tenants_domain` (`domain`),
                           UNIQUE KEY `IX_tenants_name` (`name`)
) ENGINE=InnoDB;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tenants`
--

LOCK TABLES `tenants` WRITE;
/*!40000 ALTER TABLE `tenants` DISABLE KEYS */;
INSERT INTO `tenants` VALUES ('08d99ae5-bc22-4682-8c6c-8f3b888c45dd','2021-10-29 14:09:34.374320','2021-10-30 05:00:21.498326','Kea Baby Shop','KEA','kea@kea.com','Sinza Madukani',NULL,NULL,'Baby Shop','kea','Server=localhost; Port=3306; Database=shopper; Uid=ritfol; Password=@Zakayo0810; Allow User Variables=true',NULL,2,'2022-10-29 14:09:34.374315',1),('08d99ae5-bc26-46d2-88fa-470a234dca4c','2021-10-29 14:09:34.374470','2021-10-30 05:00:21.498326','Localhost Shop','LCH','admin@shopper.com','Localhost',NULL,NULL,'Localhost shop','zaks','Server=localhost; Port=3306; Database=shopper; Uid=ritfol; Password=@Zakayo0810; Allow User Variables=true',NULL,2,'2022-10-29 14:09:34.374470',1),('08d99b6c-c4e1-45e6-85a3-fa2cd7407e73','2021-10-30 06:16:11.150906','2021-10-30 06:16:11.162859','Ijen Store','IJEN',NULL,NULL,NULL,NULL,NULL,'ijen','Server=localhost; Port=3306; Database=shopper; Uid=ritfol; Password=@Zakayo0810; Allow User Variables=true',NULL,0,'2022-10-30 06:16:11.150905',1),('08d99b82-0aa5-4862-877c-71902a3dfccc','2021-10-30 08:48:27.629851','2021-10-30 08:55:58.142543','Zakayo Shop','ZAKA',NULL,NULL,NULL,NULL,NULL,'zaka','Server=localhost; Port=3306; Database=shopper; Uid=ritfol; Password=@Zakayo0810; Allow User Variables=true','1635584158.jpg',0,'2022-10-30 08:48:27.629849',1);
/*!40000 ALTER TABLE `tenants` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_claims`
--

DROP TABLE IF EXISTS `user_claims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_claims` (
                               `Id` int NOT NULL AUTO_INCREMENT,
                               `ClaimType` longtext,
                               `ClaimValue` longtext,
                               `user_id` bigint NOT NULL,
                               PRIMARY KEY (`Id`),
                               KEY `IX_user_claims_user_id` (`user_id`),
                               CONSTRAINT `FK_user_claims_users_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_claims`
--

LOCK TABLES `user_claims` WRITE;
/*!40000 ALTER TABLE `user_claims` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_claims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_logins`
--

DROP TABLE IF EXISTS `user_logins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_logins` (
                               `LoginProvider` varchar(255) NOT NULL,
                               `ProviderKey` varchar(255) NOT NULL,
                               `ProviderDisplayName` longtext,
                               `user_id` bigint NOT NULL,
                               PRIMARY KEY (`LoginProvider`,`ProviderKey`),
                               KEY `IX_user_logins_user_id` (`user_id`),
                               CONSTRAINT `FK_user_logins_users_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_logins`
--

LOCK TABLES `user_logins` WRITE;
/*!40000 ALTER TABLE `user_logins` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_logins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_role`
--

DROP TABLE IF EXISTS `user_role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_role` (
                             `user_id` bigint NOT NULL,
                             `role_id` bigint NOT NULL,
                             PRIMARY KEY (`user_id`,`role_id`),
                             KEY `IX_user_role_role_id` (`role_id`),
                             CONSTRAINT `FK_user_role_roles_role_id` FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`) ON DELETE CASCADE,
                             CONSTRAINT `FK_user_role_users_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_role`
--

LOCK TABLES `user_role` WRITE;
/*!40000 ALTER TABLE `user_role` DISABLE KEYS */;
INSERT INTO `user_role` VALUES (1,1),(2,2),(14,13),(15,14);
/*!40000 ALTER TABLE `user_role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_tokens`
--

DROP TABLE IF EXISTS `user_tokens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_tokens` (
                               `user_id` bigint NOT NULL,
                               `login_provider` varchar(255) NOT NULL,
                               `name` varchar(255) NOT NULL,
                               `value` longtext,
                               PRIMARY KEY (`user_id`,`login_provider`,`name`),
                               CONSTRAINT `FK_user_tokens_users_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_tokens`
--

LOCK TABLES `user_tokens` WRITE;
/*!40000 ALTER TABLE `user_tokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_tokens` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
                         `id` bigint NOT NULL AUTO_INCREMENT,
                         `user_name` varchar(256) DEFAULT NULL,
                         `normalized_user_name` varchar(256) DEFAULT NULL,
                         `full_name` longtext,
                         `email` varchar(256) DEFAULT NULL,
                         `normalized_email` varchar(256) DEFAULT NULL,
                         `email_confirmed` tinyint(1) NOT NULL,
                         `password_hash` longtext,
                         `security_stamp` longtext,
                         `concurrency_stamp` longtext,
                         `phone_number` longtext,
                         `phone_number_confirmed` tinyint(1) NOT NULL,
                         `two_factor_enabled` tinyint(1) NOT NULL,
                         `lockout_end` datetime(6) DEFAULT NULL,
                         `lockout_enabled` tinyint(1) NOT NULL,
                         `access_failed_count` int NOT NULL,
                         `is_deleted` tinyint(1) NOT NULL,
                         `has_reset_password` tinyint(1) NOT NULL,
                         `tenant_id` char(36) NOT NULL,
                         `created_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
                         `updated_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
                         PRIMARY KEY (`id`),
                         UNIQUE KEY `UserNameIndex` (`normalized_user_name`),
                         KEY `EmailIndex` (`normalized_email`),
                         KEY `IX_users_tenant_id` (`tenant_id`),
                         CONSTRAINT `FK_users_tenants_tenant_id` FOREIGN KEY (`tenant_id`) REFERENCES `tenants` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=17;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'keaadmin','KEAADMIN','Ella Maira','ellagabbymaira@gmail.com','ELLAGABBYMAIRA@GMAIL.COM',1,'AQAAAAEAACcQAAAAEIUf9yhkZcWMUhKkDr8o6ZBrLvbVdy3MIFGCpAb8ZjAArd4G8twgv9YLx1nt/Z5tOQ==','VRJTXE4DZCDYWQ7BBWZYDYHQKJWW23NT',NULL,NULL,0,0,NULL,1,0,0,1,'08d99ae5-bc22-4682-8c6c-8f3b888c45dd','2021-10-29 14:09:34.639644','2021-10-29 14:09:34.776198'),(2,'localhostadmin','LOCALHOSTADMIN','Admin Localhost','admin@localhost.com','ADMIN@LOCALHOST.COM',1,'AQAAAAEAACcQAAAAEPVyheB+0+EbLTyhMrLOCq5JZXPUwVawe4YO7RGwFjadYj6+Pn/j0yhBi8EF2P3aBQ==','SXXECVKKUHJM6XU2SWNLTNDCT22YSYP4',NULL,NULL,0,0,NULL,1,0,0,1,'08d99ae5-bc26-46d2-88fa-470a234dca4c','2021-10-29 14:09:34.639772','2021-10-29 14:09:34.866785'),(14,'JohnWilfred','JOHNWILFRED','John Wilfred','jwijen@qq.com','JWIJEN@QQ.COM',1,'AQAAAAEAACcQAAAAEPmOGCW/XTCJvwfa/pTmSKaa07LIgoLTCk22Yycp6RFLpdY4PExUaDhX0Y1AAH5g/g==','USNV7UB35FEE63L4QCFU6WS6RT664ZNW','e1198f46-6d86-4973-8e31-491f2168e4ff',NULL,0,0,NULL,1,0,0,1,'08d99b6c-c4e1-45e6-85a3-fa2cd7407e73','2021-10-30 06:16:11.320071','2021-11-08 10:36:10.942273'),(15,'ZakayoHaule','ZAKAYOHAULE','Zakayo Haule','zackhaule@gmail.com','ZACKHAULE@GMAIL.COM',1,'AQAAAAEAACcQAAAAEAAJOBpXezPNKKNmC6LBvSkjo13T6hOVG6XmOk6KDdD5y79p/Z515JPP3R1TDeeQhQ==','WLNUK3KCX37VUWYM3F4R6FWOFEQ6I62E','484e0dd7-2de4-426e-b3fa-4002b3a3369c',NULL,0,0,NULL,1,0,0,1,'08d99b82-0aa5-4862-877c-71902a3dfccc','2021-10-30 08:48:27.773830','2021-11-11 16:22:14.295760');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-11-11 18:51:56
