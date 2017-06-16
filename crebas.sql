/*==============================================================*/
/* DBMS name:      ORACLE Version 11g                           */
/* Created on:     10.06.2017 2:22:16                           */
/*==============================================================*/

/*commit test*/
/*commit second*/
alter table BOARDING_PASS
   drop constraint FK_BOARDING_FLIGHT_IN_FLIGHTS;

alter table BOARDING_PASS
   drop constraint FK_BOARDING_OWNER_CLIENTS;

alter table FLIGHTS
   drop constraint FK_FLIGHTS_ARRIVAL_AIRPORTS;

alter table FLIGHTS
   drop constraint FK_FLIGHTS_DEPARTURE_AIRPORTS;

alter table FLIGHTS
   drop constraint FK_FLIGHTS_FLIFHT_S__COMPANIE;

drop table AIRPORTS cascade constraints;

drop index FLIGHT_IN_BOARDING_PASS_FK;

drop index OWNER_FK;

drop table BOARDING_PASS cascade constraints;

drop table CLIENTS cascade constraints;

drop table COMPANIES cascade constraints;

drop index DEPARTURE_FK;

drop index FLIFHT_S_COMPANY_FK;

drop index ARRIVAL_FK;

drop table FLIGHTS cascade constraints;

/*==============================================================*/
/* Table: AIRPORTS                                              */
/*==============================================================*/
create table AIRPORTS 
(
   AIRPORT_CODE         CHAR(3)              not null,
   AIRPORT_NAME         CHAR(50),
   AIRPORT_CITY         CHAR(50),
   AIRPORT_COUNTRY      CHAR(50),
   constraint PK_AIRPORTS primary key (AIRPORT_CODE)
);

/*==============================================================*/
/* Table: BOARDING_PASS                                         */
/*==============================================================*/
create table BOARDING_PASS 
(
   CLIENT_EMAIL         CHAR(50)             not null,
   FLIGHT_NUMBER        INTEGER              not null,
   BP_PLACE             CHAR(3),
   BP_TYPE              CHAR(20),
   BP_PRICE             INTEGER
);

/*==============================================================*/
/* Index: OWNER_FK                                              */
/*==============================================================*/
create index OWNER_FK on BOARDING_PASS (
   CLIENT_EMAIL ASC
);

/*==============================================================*/
/* Index: FLIGHT_IN_BOARDING_PASS_FK                            */
/*==============================================================*/
create index FLIGHT_IN_BOARDING_PASS_FK on BOARDING_PASS (
   FLIGHT_NUMBER ASC
);

/*==============================================================*/
/* Table: CLIENTS                                               */
/*==============================================================*/
create table CLIENTS 
(
   CLIENT_EMAIL         CHAR(50)             not null,
   CLIENT_PASS          CHAR(50),
   CLIENT_FIRSTNAME     CHAR(20),
   ATTRIBUTE_4          CHAR(20),
   ATTRIBUTE_5          DATE,
   constraint PK_CLIENTS primary key (CLIENT_EMAIL)
);

/*==============================================================*/
/* Table: COMPANIES                                             */
/*==============================================================*/
create table COMPANIES 
(
   COMPANY_CODE         CHAR(3)              not null,
   COMPANY_COUNTRY      CHAR(20),
   COMPANY_EMAIL        CHAR(50),
   COMPANY_PASS         CHAR(50),
   COMPANY_NAME         CHAR(50),
   constraint PK_COMPANIES primary key (COMPANY_CODE)
);

/*==============================================================*/
/* Table: FLIGHTS                                               */
/*==============================================================*/
create table FLIGHTS 
(
   FLIGHT_NUMBER        INTEGER              not null,
   AIRPORT_CODE         CHAR(3)              not null,
   COMPANY_CODE         CHAR(3)              not null,
   AIR_AIRPORT_CODE     CHAR(3)              not null,
   FLIGHT_DEPARTURE_TIME TIMESTAMP,
   FLIGHT_ARIVAL_TIME   TIMESTAMP,
   constraint PK_FLIGHTS primary key (FLIGHT_NUMBER)
);

/*==============================================================*/
/* Index: ARRIVAL_FK                                            */
/*==============================================================*/
create index ARRIVAL_FK on FLIGHTS (
   AIRPORT_CODE ASC
);

/*==============================================================*/
/* Index: FLIFHT_S_COMPANY_FK                                   */
/*==============================================================*/
create index FLIFHT_S_COMPANY_FK on FLIGHTS (
   COMPANY_CODE ASC
);

/*==============================================================*/
/* Index: DEPARTURE_FK                                          */
/*==============================================================*/
create index DEPARTURE_FK on FLIGHTS (
   AIR_AIRPORT_CODE ASC
);

alter table BOARDING_PASS
   add constraint FK_BOARDING_FLIGHT_IN_FLIGHTS foreign key (FLIGHT_NUMBER)
      references FLIGHTS (FLIGHT_NUMBER);

alter table BOARDING_PASS
   add constraint FK_BOARDING_OWNER_CLIENTS foreign key (CLIENT_EMAIL)
      references CLIENTS (CLIENT_EMAIL);

alter table FLIGHTS
   add constraint FK_FLIGHTS_ARRIVAL_AIRPORTS foreign key (AIRPORT_CODE)
      references AIRPORTS (AIRPORT_CODE);

alter table FLIGHTS
   add constraint FK_FLIGHTS_DEPARTURE_AIRPORTS foreign key (AIR_AIRPORT_CODE)
      references AIRPORTS (AIRPORT_CODE);

alter table FLIGHTS
   add constraint FK_FLIGHTS_FLIFHT_S__COMPANIE foreign key (COMPANY_CODE)
      references COMPANIES (COMPANY_CODE);

