insert into airports values ('KC1','Chaika1','Kyiv','Ukraine');
insert into companies values ('Belarus','BA1','belavia@b.com','pass2','BelAvia');
insert into flights values ('1','12-12-2012','12-12-2012','KB1','AA1','1',1,150,1,150,'12:12','12:12');
insert into booked_flight values ('bfAsaAvia1','blname1','Cherednychenko Borys','m','TT126340','10-02-2012','1','1','cherednychenko.borys@gmail.com',150);
insert into clients values ('cherednychenko.borys@gmail.com','pass','Borys','Cherednychenko');



CREATE OR REPLACE FORCE VIEW "AVAILABLE_FROM_AIRPORT" ("AIRPORT_CITY", "AIRPORT_COUNTRY", "FLIGHT_DEPARTURE") AS  (select airports.airport_city, airports.airport_country, flights.flight_departure from airports join flights on airports.airport_code = flights.flight_departure);