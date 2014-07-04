Create procedure [dbo].[TokenTemporaryPersistenceGetById]
		@id as [varchar](255)
	as
	begin
		select	
				[tokenId]
				,[tokenCreated]
				,[tokenObjectSerialized]
		from  
				[dbo].[VsixMvcAppResult.Database.TokenTemporary]
		where
				[tokenId] = @id
	end