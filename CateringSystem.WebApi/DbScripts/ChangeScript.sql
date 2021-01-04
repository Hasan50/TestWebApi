--12-nov-2020
CREATE TABLE [dbo].[UnitOfMeasurement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[ShortCode] [varchar](20) NOT NULL,
 CONSTRAINT [PK_UnitOfMeasurement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE RawItem add WeightUomId int
ALTER TABLE RawItem ADD CONSTRAINT FK_RawItem_UnitOfMeasurement_WeightUomId FOREIGN KEY (WeightUomId) 
REFERENCES dbo.UnitOfMeasurement(Id)

ALTER TABLE RawItem add QuantityUomId int
ALTER TABLE RawItem ADD CONSTRAINT FK_RawItem_UnitOfMeasurement_QuantityUomId FOREIGN KEY (QuantityUomId) 
REFERENCES dbo.UnitOfMeasurement(Id)

--29-nov-2020
ALTER TABLE PackageWithProductMasterDetail add WeightUomId int
ALTER TABLE PackageWithProductMasterDetail ADD CONSTRAINT FK_PackageWithProductMasterDetail_UnitOfMeasurement_WeightUomId FOREIGN KEY (WeightUomId) 
REFERENCES dbo.UnitOfMeasurement(Id)

ALTER TABLE PackageWithProductMasterDetail add QuantityUomId int
ALTER TABLE PackageWithProductMasterDetail ADD CONSTRAINT FK_PackageWithProductMasterDetail_UnitOfMeasurement_QuantityUomId FOREIGN KEY (QuantityUomId) 
REFERENCES dbo.UnitOfMeasurement(Id)
--30-Nov-2020

  alter table CustomerInvoice add FromName nvarchar(200) null  
  alter table CustomerInvoice add FromEmail nvarchar(200) null  
  alter table CustomerInvoice add FromPhoneNumber nvarchar(200) null  
  alter table CustomerInvoice add FromAddress nvarchar(250) null