import xlwt
import csv

'''
CREATE TABLE video(
    aid INT,
    status INT,
    title VARCHAR(200),
    pubdate DATETIME,
    mid INT,
    duration INT,
    view INT,
    dannmaku INT,
    reply INT,
    favorite INT,
    coin INT,
    share INT,
    now_rank INT,
    his_rank INT,
    support INT,
    dislike INT,
    no_reprint INT,
    copyright INT,
    PRIMARY KEY(aid)
)charset=utf8;
'''

if __name__ == '__main__':
	wbk = xlwt.Workbook()
	sheet = wbk.add_sheet('bilibili_video')
	with open("data.csv", 'r', encoding='utf-8') as file:
		videos = csv.reader(file)
		# add table header
		sheet.write(0,0,"aid")
		sheet.write(0,1,"title")
		sheet.write(0,2,"pubdate")
		sheet.write(0,3,"duration")
		sheet.write(0,4,"view")
		sheet.write(0,5,"dannmaku")
		sheet.write(0,6,"reply")
		sheet.write(0,7,"favorite")
		sheet.write(0,8,"coin")
		sheet.write(0,9,"share")
		for i, video in enumerate(videos):
			sheet.write(i+1,0,video[0])		# aid
			sheet.write(i+1,1,video[2])		# title
			sheet.write(i+1,2,video[3])		# pubdate
			sheet.write(i+1,3,video[5])		# duration
			sheet.write(i+1,4,video[6])		# view
			sheet.write(i+1,5,video[7])		# dannmaku
			sheet.write(i+1,6,video[8])		# reply
			sheet.write(i+1,7,video[9])		# favorite
			sheet.write(i+1,8,video[10])	# coin
			sheet.write(i+1,9,video[11])	# share
		wbk.save('data.xls')