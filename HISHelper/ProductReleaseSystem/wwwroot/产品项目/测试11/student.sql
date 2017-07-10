/*
SQLyog Enterprise v12.09 (64 bit)
MySQL - 5.7.12-log : Database - student
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`student` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `student`;

/*Table structure for table `student` */

DROP TABLE IF EXISTS `student`;

CREATE TABLE `student` (
  `SID` int(11) NOT NULL AUTO_INCREMENT COMMENT '学生ID',
  `Sname` varchar(13) NOT NULL COMMENT '学生姓名',
  `Sno` varchar(50) NOT NULL COMMENT '学生学号',
  `Ssex` varchar(9) NOT NULL COMMENT '性别（1：男2：女）',
  `Sage` int(11) NOT NULL COMMENT '年龄',
  `Sphone` int(13) NOT NULL COMMENT '电话',
  `Semail` varchar(30) NOT NULL COMMENT '邮箱',
  `Saddress` varchar(30) NOT NULL COMMENT '地址',
  `ScreateTime` time NOT NULL COMMENT '创建时间',
  `SupdateTime` time NOT NULL COMMENT '更新时间',
  `SentranceTime` time NOT NULL COMMENT '入学时间',
  PRIMARY KEY (`SID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Data for the table `student` */

insert  into `student`(`SID`,`Sname`,`Sno`,`Ssex`,`Sage`,`Sphone`,`Semail`,`Saddress`,`ScreateTime`,`SupdateTime`,`SentranceTime`) values (1,'sas','1','男',20,255665226,'dsds@q.com','北京','10:44:20','10:44:20','10:44:20'),(2,'dsda','2','男',21,156165165,'62dsds@q.com','北京','10:44:20','10:44:20','10:44:20'),(3,'dsadas','3','男',26,111111111,'fdsfs@qq.com','北京','10:44:20','10:44:20','10:44:20'),(4,'asda','4','男',22,3123123,'dafa@qq.com','北京','10:44:20','10:44:20','10:44:20');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
