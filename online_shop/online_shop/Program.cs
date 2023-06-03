using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
namespace AdoNetConsoleApp
{
    class Program
    {
        static string connectionString = "Database = shop;Data Source=localhost;port=3306;User Id=root; password=Aa123456";
        static void Create(string query)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
        }
        static void CreateBD()
        {
            Create(@"CREATE SCHEMA `shop`");


            Create(@"CREATE TABLE `shop`.`product` (
            `id` INT NOT NULL,
            `name` VARCHAR(45) NOT NULL,
            `price` VARCHAR(45) NOT NULL,
             PRIMARY KEY (`id`))");

            Create(@"CREATE TABLE `shop`.`order` (
            `id` INT NOT NULL,
            `FIO` VARCHAR(45) NOT NULL,
            `address` VARCHAR(45) NOT NULL,
            PRIMARY KEY (`id`))");

            Create(@"CREATE TABLE `shop`.`order_items` (
           `id` INT NOT NULL,
           `order_id` INT NOT NULL,
           `product_id` INT NOT NULL,
           `amount` INT NOT NULL,
            PRIMARY KEY (`id`))");

            Create(@"ALTER TABLE `shop`.`order_items` 
            ADD INDEX `order_id_idx` (`order_id` ASC) VISIBLE,
            ADD INDEX `product_id_idx` (`product_id` ASC) VISIBLE;
            ALTER TABLE `shop`.`order_items` 
            ADD CONSTRAINT `order_id`
            FOREIGN KEY (`order_id`)
            REFERENCES `shop`.`order` (`id`)
            ON DELETE NO ACTION
            ON UPDATE NO ACTION,
            ADD CONSTRAINT `product_id`
            FOREIGN KEY (`product_id`)
            REFERENCES `shop`.`product` (`id`)
            ON DELETE NO ACTION
            ON UPDATE NO ACTION;
            ");
        }
        static void InsertToTables()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand(
            @"INSERT INTO `shop`.`product` (`id`, `name`, `price`) VALUES ('1', 'Пицца', '300');
            INSERT INTO `shop`.`product` (`id`, `name`, `price`) VALUES ('2', 'Роллы', '280');
            INSERT INTO `shop`.`product` (`id`, `name`, `price`) VALUES ('3', 'Гамбургер', '160');

            INSERT INTO `shop`.`order` (`id`, `FIO`,`address`) VALUES ('1', 'Вероника','Киевская');
            INSERT INTO `shop`.`order` (`id`, `FIO`,`address`) VALUES ('2', 'Роза','Шмулевича'); 
            INSERT INTO `shop`.`order` (`id`, `FIO`,`address`) VALUES ('3', 'Илона','Калоева'); 

            INSERT INTO `shop`.`order_items` (`id`, `order_id`, `product_id`, `amount`) VALUES ('1', '3', '2', '1');
            INSERT INTO `shop`.`order_items` (`id`, `oder_id`, `product_id`, `amount`) VALUES ('2', '2', '3', '1');
            INSERT INTO `shop`.`order_items` (`id`, `order_id`, `product_id`, `amount`) VALUES ('3', '1', '2', '1');", connection);

            command.ExecuteNonQuery();
        }
        static void Update()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand(
            @"UPDATE shop.order_items
            set order_id = 4, product_id =3,amount=1;", connection);
            command.ExecuteNonQuery();
        }

        static void Main(string[] args)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                while (true)
                {
                    Console.WriteLine("Greate");
                    Console.WriteLine("AddToTables");
                    Console.WriteLine("Update");
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("Подключение закрыто");
            }
        }
    }
}