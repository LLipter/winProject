load data infile 'C:\\ProgramData\\MySQL\\MySQL Server 5.7\\Uploads\\data.csv'
into table video
fields terminated by ','
optionally enclosed by '"'
lines terminated by '\r\n';
