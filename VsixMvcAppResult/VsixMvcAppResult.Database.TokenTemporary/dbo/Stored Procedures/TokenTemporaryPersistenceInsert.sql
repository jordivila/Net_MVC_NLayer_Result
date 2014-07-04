Create procedure [dbo].[TokenTemporaryPersistenceInsert]
		@id as [varchar](255),
		@tokenCreated [datetime],
		@tokenObjectSerialized [varchar](max)
	as
	begin

		insert into [dbo].[VsixMvcAppResult.Database.TokenTemporary] ([tokenId], [tokenCreated] ,[tokenObjectSerialized])
		select	@id, @tokenCreated, @tokenObjectSerialized

	end