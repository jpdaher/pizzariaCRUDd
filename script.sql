
SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

DROP SCHEMA IF EXISTS `pizzaria` ;

CREATE SCHEMA IF NOT EXISTS `pizzaria` DEFAULT CHARACTER SET utf8 ;
USE `pizzaria` ;

DROP TABLE IF EXISTS `pizzaria`.`clientes` ;

CREATE TABLE IF NOT EXISTS `pizzaria`.`clientes` (
  `id_cliente` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(255) NOT NULL,
  `telefone` VARCHAR(15) NOT NULL,
  `endereco` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`id_cliente`),
  UNIQUE INDEX `id_cliente_UNIQUE` (`id_cliente` ASC) VISIBLE,
  UNIQUE INDEX `nome_UNIQUE` (`nome` ASC) VISIBLE)
ENGINE = InnoDB;


DROP TABLE IF EXISTS `pizzaria`.`pizzas` ;

CREATE TABLE IF NOT EXISTS `pizzaria`.`pizzas` (
  `id_pizza` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(255) NOT NULL,
  `descricao` VARCHAR(255) NOT NULL,
  `preco` DOUBLE NOT NULL,
  PRIMARY KEY (`id_pizza`),
  UNIQUE INDEX `id_pizza_UNIQUE` (`id_pizza` ASC) VISIBLE)
ENGINE = InnoDB;


DROP TABLE IF EXISTS `pizzaria`.`pedidos` ;

CREATE TABLE IF NOT EXISTS `pizzaria`.`pedidos` (
  `id_pedido` INT NOT NULL AUTO_INCREMENT,
  `data_pedido` DATETIME NOT NULL,
  `id_cliente` INT NOT NULL,
  `id_pizza` INT NOT NULL,
  PRIMARY KEY (`id_pedido`),
  UNIQUE INDEX `id_pedido_UNIQUE` (`id_pedido` ASC) VISIBLE,
  INDEX `fk_pedidos_clientes_idx` (`id_cliente` ASC) VISIBLE,
  INDEX `fk_pedidos_pizzas1_idx` (`id_pizza` ASC) VISIBLE,
  CONSTRAINT `fk_pedidos_clientes`
    FOREIGN KEY (`id_cliente`)
    REFERENCES `pizzaria`.`clientes` (`id_cliente`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_pedidos_pizzas1`
    FOREIGN KEY (`id_pizza`)
    REFERENCES `pizzaria`.`pizzas` (`id_pizza`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

INSERT INTO pizzas (nome, descricao, preco) VALUES ("Margherita", "Molho, Queijo, Tomate, Manjericão", 11.99);
INSERT INTO pizzas (nome, descricao, preco) VALUES ("Calabresa", "Molho, Queijo, Calabresa, Cebola", 11.99);
INSERT INTO pizzas (nome, descricao, preco) VALUES ("Portuguesa", "Molho, Queijo, Presunto, Ovo, Palmito, Pimentão", 12.99);

INSERT INTO clientes (nome, telefone, endereco) VALUES ("João", "1111", "Rua do João");
INSERT INTO clientes (nome, telefone, endereco) VALUES ("Wagner", "2222", "Rua do Wagner");
INSERT INTO clientes (nome, telefone, endereco) VALUES ("José", "3333", "Rua do José");
