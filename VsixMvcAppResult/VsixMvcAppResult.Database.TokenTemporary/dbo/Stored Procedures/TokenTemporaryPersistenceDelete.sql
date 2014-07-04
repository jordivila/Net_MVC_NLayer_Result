Create procedure [dbo].TokenTemporaryPersistenceDelete
		@id as [varchar](255)
	as
	begin

		delete from [dbo].[VsixMvcAppResult.Database.TokenTemporary] where tokenId = @id

		select @@rowcount

	end