CREATE TABLE [dbo].[VsixMvcAppResult.Database.TokenTemporary]
(
	[tokenId] NVARCHAR(255) NOT NULL PRIMARY KEY, 
    [tokenCreated] DATETIME NOT NULL, 
    [tokenObjectSerialized] NVARCHAR(MAX) NOT NULL
)

GO


