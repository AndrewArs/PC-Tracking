-- phpMyAdmin SQL Dump
-- version 4.6.6
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Jun 02, 2017 at 12:15 PM
-- Server version: 10.1.20-MariaDB
-- PHP Version: 7.0.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `id1718001_logdb`
--

-- --------------------------------------------------------

--
-- Table structure for table `LogDB`
--

CREATE TABLE `LogDB` (
  `id` int(11) NOT NULL,
  `Date` varchar(25) COLLATE utf8_unicode_ci NOT NULL,
  `Operation` varchar(25) COLLATE utf8_unicode_ci NOT NULL,
  `HWID` varchar(25) COLLATE utf8_unicode_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `LogDB`
--

INSERT INTO `LogDB` (`id`, `Date`, `Operation`, `HWID`) VALUES
(3, '25.05.2017 18:23:16', 'Program started', '178BFBFF00500F20'),
(4, '25.05.2017 19:42:07', 'Low', '178BFBFF00500F20'),
(5, '25.05.2017 19:43:37', 'Power line plugged', '178BFBFF00500F20'),
(6, '25.05.2017 19:43:38', 'Low, Charging', '178BFBFF00500F20'),
(7, '25.05.2017 22:12:34', 'High', '178BFBFF00500F20'),
(8, '26.05.2017 8:57:09', 'Program started', '178BFBFF00500F20'),
(9, '26.05.2017 9:45:45', 'Suspend', '178BFBFF00500F20'),
(10, '26.05.2017 10:12:09', 'Resume', '178BFBFF00500F20'),
(20, '27.05.2017 14:38:32', 'Program started', '178BFBFF00500F20'),
(21, '27.05.2017 14:46:19', 'Program closed', '178BFBFF00500F20'),
(22, '27.05.2017 14:49:56', 'Program started', '178BFBFF00500F20'),
(23, '27.05.2017 14:57:43', 'Program started', '178BFBFF00500F20'),
(24, '27.05.2017 14:58:26', 'Program closed', '178BFBFF00500F20'),
(25, '27.05.2017 14:58:51', 'Program started', '178BFBFF00500F20'),
(26, '27.05.2017 15:00:27', 'Program closed', '178BFBFF00500F20'),
(35, '27.05.2017 18:46:03', 'Program started', '178BFBFF00500F20'),
(36, '27.05.2017 21:03:16', 'High', '178BFBFF00500F20'),
(37, '27.05.2017 22:35:16', 'Resume', '178BFBFF00500F20'),
(38, '27.05.2017 22:41:56', 'Power line unplugged', '178BFBFF00500F20'),
(39, '27.05.2017 22:56:42', 'Suspend', '178BFBFF00500F20'),
(40, '27.05.2017 23:46:56', 'Resume', '178BFBFF00500F20'),
(41, '28.05.2017 0:39:21', 'Low, Charging', '178BFBFF00500F20'),
(42, '29.05.2017 17:25:07', 'Power line unplugged', '178BFBFF00500F20'),
(43, '29.05.2017 18:17:12', 'Power line plugged', '178BFBFF00500F20'),
(44, '29.05.2017 18:17:23', 'Charging', '178BFBFF00500F20'),
(45, '29.05.2017 20:00:02', 'High', '178BFBFF00500F20'),
(46, '29.05.2017 20:48:11', 'Suspend', '178BFBFF00500F20'),
(47, '30.05.2017 0:51:02', 'Resume', '178BFBFF00500F20'),
(48, '30.05.2017 9:58:17', 'Power line unplugged', '178BFBFF00500F20'),
(49, '30.05.2017 10:37:43', 'Suspend', '178BFBFF00500F20'),
(50, '30.05.2017 10:42:36', 'Resume', '178BFBFF00500F20'),
(51, '31.05.2017 0:22:58', 'Power line unplugged', '178BFBFF00500F20'),
(52, '31.05.2017 0:42:18', 'Suspend', '178BFBFF00500F20'),
(53, '31.05.2017 0:54:43', 'Resume', '178BFBFF00500F20'),
(54, '31.05.2017 0:55:14', 'Program closed', '178BFBFF00500F20'),
(59, '31.05.2017 12:57:19', 'High', '178BFBFF00500F20'),
(68, '31.05.2017 16:07:59', 'Program started', '178BFBFF00500F10'),
(69, '31.05.2017 16:08:05', 'Power line unplugged', '178BFBFF00500F10'),
(70, '31.05.2017 16:08:06', 'Power line plugged', '178BFBFF00500F10'),
(71, '31.05.2017 16:08:12', 'NoSystemBattery', '178BFBFF00500F10'),
(72, '31.05.2017 16:08:18', 'High, Charging', '178BFBFF00500F10'),
(73, '31.05.2017 16:29:08', 'Program closed', '178BFBFF00500F10'),
(74, '31.05.2017 16:47:22', 'Program closed', '178BFBFF00500F20'),
(75, '31.05.2017 17:00:28', 'Program started', '178BFBFF00500F10'),
(76, '31.05.2017 17:00:36', 'Program closed', '178BFBFF00500F10'),
(77, '31.05.2017 11:29:22', 'Program started', '178BFBFF00500F20'),
(78, '31.05.2017 17:08:53', 'Program started', '178BFBFF00500F20'),
(79, '31.05.2017 17:11:19', 'Program started', '178BFBFF00500F20'),
(80, '31.05.2017 17:11:47', 'Program started', '178BFBFF00500F20'),
(81, '31.05.2017 17:11:53', 'Program closed', '178BFBFF00500F20'),
(82, '31.05.2017 17:12:46', 'Program closed', '178BFBFF00500F20'),
(83, '01.06.2017 13:03:52', 'Program started', '178BFBFF00500F20'),
(84, '01.06.2017 18:27:47', 'Program started', '178BFBFF00500F20'),
(85, '01.06.2017 18:51:00', 'Program closed', '178BFBFF00500F20'),
(86, '02.06.2017 13:02:35', 'Program started', '178BFBFF00500F20'),
(87, '02.06.2017 13:05:04', 'Power line plugged', '178BFBFF00500F20'),
(88, '02.06.2017 13:05:05', 'High, Charging', '178BFBFF00500F20'),
(89, '02.06.2017 13:20:49', 'Program started', '178BFBFF00500F20'),
(90, '02.06.2017 13:59:50', 'High', '178BFBFF00500F20');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `LogDB`
--
ALTER TABLE `LogDB`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `LogDB`
--
ALTER TABLE `LogDB`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=91;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
