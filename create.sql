
  CREATE TABLE "AIRPORTS" 
   (	"AIRPORT_CODE" CHAR(3 BYTE) NOT NULL ENABLE, 
	"AIRPORT_NAME" CHAR(50 BYTE) NOT NULL ENABLE, 
	"AIRPORT_CITY" CHAR(20 BYTE) NOT NULL ENABLE, 
	"AIRPORT_COUNTRY" CHAR(20 BYTE) NOT NULL ENABLE, 
	 CONSTRAINT "AIRPORT_PK" PRIMARY KEY ("AIRPORT_CODE")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;



  CREATE TABLE "COMPANIES" 
   (	"COMPANY_COUNTRY" CHAR(20 BYTE) NOT NULL ENABLE, 
	"COMPANY_CODE" CHAR(3 BYTE) NOT NULL ENABLE, 
	"COMPANY_EMAIL" CHAR(50 BYTE) NOT NULL ENABLE, 
	"COMPANY_PASS" CHAR(50 BYTE) NOT NULL ENABLE, 
	"COMPANY_NAME" CHAR(50 BYTE) NOT NULL ENABLE, 
	 CONSTRAINT "COMPANIES_PK" PRIMARY KEY ("COMPANY_CODE")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;



  CREATE TABLE "CLIENTS" 
   (	"CLIENT_EMAIL" CHAR(50 BYTE) NOT NULL ENABLE, 
	"CLIENT_PASS" CHAR(50 BYTE) NOT NULL ENABLE, 
	"CLIENT_FIRSTNAME" CHAR(20 BYTE) NOT NULL ENABLE, 
	"CLIENT_LASTNAME" CHAR(20 BYTE) NOT NULL ENABLE, 
	 CONSTRAINT "CLIENT_PK" PRIMARY KEY ("CLIENT_EMAIL")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;


  CREATE TABLE "FLIGHTS" 
   (	"FLIGHT_NUMBER" CHAR(8 BYTE), 
	"FLIGHT_DEPARTURE_TIME" DATE, 
	"FLIGHT_ARRIVAL_TIME" DATE, 
	"FLIGHT_DEPARTURE" CHAR(3 BYTE) NOT NULL ENABLE, 
	"FLIGHT_COMP" CHAR(3 BYTE) NOT NULL ENABLE, 
	"FLIGHT_ARRIVAL" CHAR(3 BYTE) NOT NULL ENABLE, 
	"FLIGHT_E_QUANTITY" NUMBER(2,0) NOT NULL ENABLE, 
	"FLIGHT_E_PRICE" NUMBER(6,0), 
	"FLIGHT_B_QUANTITY" NUMBER(2,0) NOT NULL ENABLE, 
	"FLIGHT_B_PRICE" NUMBER, 
	"FLIGHT_D_TIME" CHAR(5 BYTE), 
	"FLIGHT_A_TIME" CHAR(5 BYTE), 
	 CONSTRAINT "FLIGHT_PK" PRIMARY KEY ("FLIGHT_NUMBER")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE, 
	 CONSTRAINT "FK_FLIGHTS_COMPANIES" FOREIGN KEY ("FLIGHT_COMP")
	  REFERENCES "COMPANIES" ("COMPANY_CODE") ENABLE, 
	 CONSTRAINT "FK_FLIGHTS_AIRPORT_D" FOREIGN KEY ("FLIGHT_DEPARTURE")
	  REFERENCES "AIRPORTS" ("AIRPORT_CODE") ENABLE
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;



  CREATE TABLE "BOOKED_FLIGHT" 
   (	"B_F_NAME" CHAR(50) NOT NULL ENABLE, 
	"B_L_NAME" CHAR(50) NOT NULL ENABLE, 
	"B_CITIZENSHIP" CHAR(50) NOT NULL ENABLE, 
	"B_GENDER" CHAR(1), 
	"B_PASSPORT" CHAR(8) NOT NULL ENABLE, 
	"B_PASS_VALID" CHAR(10) NOT NULL ENABLE, 
	"B_NUM" NUMBER(5,0) NOT NULL ENABLE, 
	"B_FLIGHT" CHAR(8) NOT NULL ENABLE, 
	"BOOKED_FLIGHT_C_EMAIL" CHAR(50), 
	"B_F_PRICE" NUMBER(6,0) NOT NULL ENABLE, 
	 CONSTRAINT "BOOKED_FLIGHT_PK" PRIMARY KEY ("B_NUM")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE, 
	 CONSTRAINT "FK_FLIGHTS_BOOKED_FLIGHT" FOREIGN KEY ("B_FLIGHT")
	  REFERENCES "FLIGHTS" ("FLIGHT_NUMBER") ENABLE
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;




  CREATE OR REPLACE FORCE VIEW "AVAILABLE_FROM_AIRPORT" ("AIRPORT_CITY", "AIRPORT_COUNTRY", "FLIGHT_DEPARTURE") AS 
  (select airports.airport_city, airports.airport_country, flights.flight_departure
from airports join flights
on airports.airport_code = flights.flight_departure);



  CREATE OR REPLACE FORCE VIEW "AVAILABLE_TO_AIRPORT" ("AIRPORT_CITY", "AIRPORT_COUNTRY", "FLIGHT_ARRIVAL") AS 
  (select airports.airport_city, airports.airport_country, flights.flight_arrival
from airports join flights
on airports.airport_code = flights.flight_arrival);



   


    CREATE OR REPLACE FORCE VIEW "COMP_DATA" ("COMPANY_EMAIL", "COMPANY_PASS", "COMPANY_CODE") AS 
  (select company_email, company_pass,company_code  from companies);



  CREATE OR REPLACE FORCE VIEW "COMPFLIGHTS" ("FLIGHT_COMP", "FLIGHT_NUMBER", "FLIGHT_DEPARTURE", "D_T", "FLIGHT_ARRIVAL", "A_T", "FLIGHT_E_QUANTITY", "FLIGHT_E_PRICE", "FLIGHT_B_QUANTITY", "FLIGHT_B_PRICE") AS 
  (select flight_comp, flight_number, flight_departure, concat(flight_departure_time ,concat(' ',flight_d_time)) as d_t,
flight_arrival,concat(flight_arrival_time,concat(' ',flight_a_time))as a_t,flight_e_quantity,flight_e_price,
flight_b_quantity,flight_b_price
from flights);



  CREATE OR REPLACE FORCE VIEW "CUST_DATA" ("CLIENT_EMAIL", "CLIENT_PASS") AS 
  (select client_email, client_pass from clients);



  CREATE OR REPLACE FORCE VIEW "FLIGHTS1" ("FLIGHT_NUMBER", "FLIGHT_COMP", "AIRPORT_NAME", "AIRPORT_CITY", "FLIGHT_DEPARTURE_TIME", "FLIGHT_ARRIVAL", "FLIGHT_ARRIVAL_TIME", "FLIGHT_E_PRICE", "FLIGHT_B_PRICE") AS 
  (select flights.flight_number, flights.flight_comp, airports.airport_name,airports.airport_city ,FLIGHTS.FLIGHT_DEPARTURE_TIME,flights.flight_arrival, FLIGHTS.FLIGHT_ARRIVAL_time, FLIGHTS.FLIGHT_E_PRICE,flights.FLIGHT_B_PRICE
from flights 
left outer join airports
on flights.flight_departure = airports.AIRPORT_CODE);




  CREATE OR REPLACE FORCE VIEW "SUITABLE_FLIGHTS" ("FLIGHT_NUMBER", "FLIGHT_COMP", "D_NAME", "D_CITY", "FLIGHT_DEPARTURE_TIME", "A_NAME", "A_CITY", "FLIGHT_ARRIVAL_TIME", "FLIGHT_E_PRICE", "FLIGHT_B_PRICE", "D_MONTH", "D_DAY", "A_MONTH", "A_DAY") AS 
  (select Flights1.flight_number, Flights1.flight_comp, Flights1.airport_name as d_name,Flights1.airport_city as d_city ,Flights1.FLIGHT_DEPARTURE_TIME,airports.airport_name as a_name , 
AIRPORTS.AIRPORT_CITY as a_city , Flights1.FLIGHT_ARRIVAL_time, 
Flights1.FLIGHT_E_PRICE,Flights1.FLIGHT_B_PRICE,
extract(month from Flights1.FLIGHT_departure_time) as D_month,
extract(day from Flights1.FLIGHT_departure_time) as D_day,
extract(month from Flights1.FLIGHT_arrival_time) as A_month,
extract(day from Flights1.FLIGHT_arrival_time) as A_day
from Flights1 join airports
on Flights1.flight_arrival = airports.AIRPORT_CODE);

CREATE OR REPLACE FORCE VIEW "BOOKINGHISTORY" ("B_NUM", "DEPARTURE", "FLIGHT_DEPARTURE_TIME", "ARRIVAL", "B_F_PRICE", "BOOKED_FLIGHT_C_EMAIL") AS 
  (select booked_flight.b_num, concat(trim(suitable_flights.d_name),concat('. ',trim(suitable_flights.d_city))) as departure,
suitable_flights.flight_departure_time,concat(trim(suitable_flights.a_name),concat('. ',trim(suitable_flights.a_city)))as arrival,
booked_flight.b_f_price, BOOKED_FLIGHT.BOOKED_FLIGHT_C_EMAIL
from BOOKED_FLIGHT left JOIN SUITABLE_FLIGHTS
on booked_flight.b_flight = suitable_flights.flight_number);


create or replace PROCEDURE ADDFLIGHT
(
  FN IN char 
, FDT in char
, FAT in char
, FD in char
, FC in char
, FA IN char
, EQ in number
, EP IN number
, BQ in number
, BP IN number
, dtt IN VARCHAR2
, att IN VARCHAR2
) AS 
BEGIN
set transaction isolation level SERIALIZABLE  NAME 'trans';
  insert into FLIGHTS(flight_number,  flight_departure_time, flight_arrival_time, 
 flight_departure, flight_comp, flight_arrival, flight_e_quantity, flight_e_price, flight_b_quantity, 
 flight_b_price,flight_d_time, flight_a_time) values(FN, to_date(fdt,'dd.mm.yyyy'),to_date( fat,'dd.mm.yyyy'), fd, fc, fa, eq,ep,bq,bp, dtt,att);
commit;
END addflight;
/

create or replace PROCEDURE delFLIGHT
(
  
 FN IN char 

) AS 
BEGIN
set transaction isolation level SERIALIZABLE  NAME 'trans';

EXECUTE IMMEDIATE 'alter table
   Booked_flight
 DISABLE constraint
   FK_FLIGHTS_BOOKED_FLIGHT';

  delete from flights
 where flight_number = FN;

 delete from Booked_flight
 where B_Flight = FN;

 EXECUTE IMMEDIATE 'alter table
   Booked_flight
ENABLE constraint
   FK_FLIGHTS_BOOKED_FLIGHT';
commit;
END delflight;
/

create or replace PROCEDURE EDIT_CLIENT
(
  EMAIL IN VARCHAR2 
, newemail in varchar2
, fname in varchar2
, lname in varchar2
, pass IN VARCHAR2
) AS 
BEGIN
set transaction isolation level SERIALIZABLE  NAME 'trans';
 update booked_flight
  set booked_flight.BOOKED_FLIGHT_C_EMAIL= newemail
  where TRIM(BOOKED_FLIGHT_C_EMAIL) = email;
  update clients
  set CLIENT_PASS = pass,
      Client_email = newemail,
      client_firstname = fname,
      client_lastname = lname
  where TRIM(CLIENT_EMAIL) = email;
  commit;
   END EDIT_CLIENT
  ;
/

create or replace PROCEDURE EDITFLIGHT
(
  oldFN IN char
, FN IN char 
, FDT in char
, FAT in char
, FD in char
, FA IN char
, EQ in number
, EP IN number
, BQ in number
, BP IN number
, dtt IN VARCHAR2
, att IN VARCHAR2
) AS 
BEGIN
set transaction isolation level SERIALIZABLE  NAME 'trans';
EXECUTE IMMEDIATE 'alter table
   Booked_flight
 DISABLE constraint
   FK_FLIGHTS_BOOKED_FLIGHT';

  update FLIGHTS
  set flight_number = FN, 
  flight_departure_time = to_date(fdt,'dd.mm.yyyy'),
  flight_arrival_time = to_date( fat,'dd.mm.yyyy'), 
 flight_departure = fd,
 flight_arrival = fa,
 flight_e_quantity = eq,
 flight_e_price = ep, 
 flight_b_quantity = bq, 
 flight_b_price = bp,
 flight_d_time = dtt,
 flight_a_time=att
 where flight_number = oldFN;

 update Booked_flight
 set B_Flight = FN
 where B_Flight = oldFN;

 EXECUTE IMMEDIATE 'alter table
   Booked_flight
ENABLE constraint
   FK_FLIGHTS_BOOKED_FLIGHT';
commit;
END editflight;
/






