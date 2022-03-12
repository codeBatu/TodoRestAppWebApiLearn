create Table TodoInfo(
Id int primary key identity(1,1),
Title nvarchar(128) not null,
Text nvarchar(max) not null,
CreatedDateTime DateTime default(SYSDATETIME()) not null,
UpdateDateTime DateTime default (SYSDATETIME()) not null,
Completed bit default(0) not null,

)