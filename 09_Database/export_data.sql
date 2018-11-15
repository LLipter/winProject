select * from video order by view desc limit 100
into outfile '/var/lib/mysql-files/data.csv'   
fields terminated by ','
optionally enclosed by '"'
lines terminated by '\r\n';