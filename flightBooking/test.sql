ALTER TABLE FLIGHTS
ADD CONSTRAINT FK_Flights_Companies FOREIGN KEY (company_code) REFERENCES Companies (company_code);
ALTER TABLE FLIGHTS
ADD CONSTRAINT FK_Flights_Airports_arrival FOREIGN KEY (airport_code_d) REFERENCES Airports (airport_code);
ALTER TABLE FLIGHTS
ADD CONSTRAINT FK_Flights_Airports_departure FOREIGN KEY (airport_code_a) REFERENCES Airports (airport_code);

ALTER TABLE BOARDINGPASS
ADD CONSTRAINT FK_Boardingpass_Clients FOREIGN KEY (client_email) REFERENCES Clients (client_email);
ALTER TABLE BOARDINGPASS
ADD CONSTRAINT FK_Boardingpass_Flights FOREIGN KEY (flight_number) REFERENCES Flights (flight_number);