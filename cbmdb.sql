-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 23, 2024 at 05:58 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `cbmdb`
--

-- --------------------------------------------------------

--
-- Table structure for table `managetbl`
--

CREATE TABLE `managetbl` (
  `ItemID` int(11) NOT NULL,
  `Category` varchar(255) NOT NULL,
  `Item` varchar(255) NOT NULL,
  `Stock` int(11) DEFAULT NULL,
  `Manufacturer` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `managetbl`
--

INSERT INTO `managetbl` (`ItemID`, `Category`, `Item`, `Stock`, `Manufacturer`) VALUES
(1, 'Foods', 'hatdog', 50, 'betlogan'),
(6, 'Foods', 'Pancit', 50, 'JC Panciteria'),
(7, 'Foods', 'Hatdog', 50, 'betlogan'),
(8, 'Vehicle', 'monkey', 20, 'sall'),
(11, 'Foods', 'qwasong', 49, 'qwasn');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `managetbl`
--
ALTER TABLE `managetbl`
  ADD PRIMARY KEY (`ItemID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `managetbl`
--
ALTER TABLE `managetbl`
  MODIFY `ItemID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
