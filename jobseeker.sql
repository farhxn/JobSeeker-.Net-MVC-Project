use jobseeker


create table category(
ID int primary key identity(1,1),
Name varchar(520),
logo varchar(520))

create table admin(
id int primary key identity(1,1),
name varchar(max),
email varchar(max),
password varchar(max),
img varchar(max)
)

create table company(
ID int primary key identity (1,1),
Name varchar(max),
email varchar(max),
password varchar(max),
phone int,
website varchar(max),
Cover varchar(max),
logo varchar(max),
discription varchar(max),
industry varchar(max),
founded int,
size varchar(max),
country varchar(max),
state varchar(max),
address varchar(max),
facebook varchar(max),
twitter varchar(max),
linkdin varchar(max), 
instagram varchar(max),
img1 varchar(max),
img2 varchar(max),
img3 varchar(max),
img4 varchar(max)
)
create table job(
ID int primary key identity (1,1),
title varchar(max),
locat varchar(max),
category varchar(max),
overview varchar(max),
responsiblities varchar(max),
skills varchar(max),
experince varchar(max),
lvl varchar(max),
typE varchar(max),
salary varchar(max),
company varchar(max)
)

create table jobprofile(
ID int primary key identity(1,1),
name varchar(max),
email varchar(max),
password varchar(max),
title varchar(max),
location varchar(max),
phone varchar(max),
cover varchar(max),
img varchar(max),
about varchar(max),
skills1 varchar(max),
skills2 varchar(max),
skills3 varchar(max),
skills4 varchar(max),
experincetitle1 varchar(max),
company1 varchar(max), 
time1 varchar(max),
discription1 varchar(max),
experincetitle2 varchar(max),
company2 varchar(max), 
time2 varchar(max),
discription2 varchar(max),
etitle1 varchar(max),
eschool1 varchar(max),
etime1 varchar(max),
edisc1 varchar(max),
etitle2 varchar(max),
eschool2 varchar(max),
etime2 varchar(max),
edisc2 varchar(max)
)


drop table applications

create table applications(
ID int primary key identity(1,1),
username varchar(max),
userimg varchar(max),
useremail varchar(max),
userid varchar(max),
comemail varchar(max),
application_tittle varchar(max),
application_locat varchar(max)
) 


insert into category values('Business Development','fa fa-pie-chart')
insert into category values('Marketing & Communications','fa fa-bullhorn')
insert into category values('Human Resources','fa fa-address-card-o')
insert into category values('Project Management','fa fa-calendar-o')
insert into category values('Software Engineering','fa-laptop-code')
insert into category values('Customer Service','Software Engineering')
insert into category values('Marketing & Communications','fa fa-terminal')
update category set logo ='fa fa-laptop' where ID = 9delete from category where ID = 8
drop table applications
alter Table job drop column datetime
update job set locat='Karachi',title = 'Editor', category='Marketing & Communications' where ID=1 
update job set cover='company-hero-2.jpg' where ID=1 
select  company.Name as jm ,job.company as cn from company right join job on company.Name = job.company
delete from jobprofile where ID = 6
select * from jobprofile
select * from admin
Alter table job Add cover varchar(max)
ALTER TABLE job ADD Dat DATETIME NOT NULL DEFAULT (GETDATE());
truncate table company
ALTER TABLE job add cover varchar(max)
DROP COLUMN Dat
alter table job add  A VARCHAR(MAX)
select* from company
ALTER TABLE company ALTER COLUMN phone VARCHAR(max);
drop table company
ALTER TABLE job add datetime varchar(520);
ALTER TABLE job ADD DEFAULT GETDATE()	 FOR A
ALTER TABLE company
ADD FOREIGN KEY (industry) REFERENCES category(ID);
select * from job
insert into jobprofile values('','edrrr','Customer Services', '','','reeree','qwerwqer','','','','','')
select company.Name as cn ,job.company as jc,company.Cover as cc,company.logo as cl from company right join job on job.company=company.Name where job.ID =1
Select count(*) as outofproducts from job where category='Customer Services';
update company set  password='1234' where ID=1
delete from jobprofile where ID =19
update jobprofile set name = 'Ayesha' where id = 3

use jobseeker
select * from job
select * from company 
select * from category
select * from jobprofile
select * from admin
select *from applications
sele
