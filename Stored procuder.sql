
use EncryptDB

-----------Accounts-------------------------
create proc createAccount @userName varchar(50), @password varchar(50),@favorite varchar(50)
as
insert into Accounts(UserName,[Password],Favorite)
values(@userName,@password,@favorite)

create proc delete_admin_account
as
Delete  from Accounts
where username = 'admin'

----------Login----------------------
create proc getAllAccounts
as 
select * from Accounts



-----Edit Password------
create proc Edit_Password @newPassword varchar(50) , @id int
as
update Accounts
set 
[Password] = @newPassword
where id = @id

-------Encrypted table------------------
--add 
create proc Add_encrypt_file @fileName nvarchar(50) , @path nvarchar(max), @fileData varbinary(max),
@key varchar(max), @iv varchar(max), @encrypteDate varchar(50)
as 
insert into Encrypted_tbl([fileName],[path],[fileData],[key],[iv],[encrypteDate])
values (@fileName , @path , @fileData , @key , @iv , @encrypteDate)

--Get Date file Encrypt
Create proc Encrypt_Date 
as
select (encrypteDate) from Encrypted_tbl

--Get Encrypted file count
Create proc file_Count 
as
select count(id) from Encrypted_tbl

--Get All File Encrypted
create proc Get_All_Data
as 
select id,[fileName],[path],encrypteDate from Encrypted_tbl

--Get data by value(Search)
create proc Get_Data_By_Value @id int, @fileName nvarchar(50) 
as 
select id,[fileName],[path],encrypteDate from Encrypted_tbl
where id = @id  or [fileName] like @fileName + '%' 

--Get data by id

create proc Get_data_by_id @id int
as
select * from Encrypted_tbl
where id = @id

--delete data

create proc Delete_Data @id int
as 
Delete from Encrypted_tbl
where id = @id