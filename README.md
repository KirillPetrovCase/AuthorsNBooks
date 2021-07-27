# AuthorsNBooks

## Tables
### M-M relations
```
CREATE TABLE [dbo].[AuthorBook] (
    [AuthorsId] INT NOT NULL,
    [BooksId]   INT NOT NULL,
    CONSTRAINT [PK_AuthorBook] PRIMARY KEY CLUSTERED ([AuthorsId] ASC, [BooksId] ASC),
    CONSTRAINT [FK_AuthorBook_Authors_AuthorsId] FOREIGN KEY ([AuthorsId]) REFERENCES [dbo].[Authors] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AuthorBook_Books_BooksId] FOREIGN KEY ([BooksId]) REFERENCES [dbo].[Books] ([Id]) ON DELETE CASCADE
);

GO
CREATE NONCLUSTERED INDEX [IX_AuthorBook_BooksId]
    ON [dbo].[AuthorBook]([BooksId] ASC);
```

### Author table
```
CREATE TABLE [dbo].[Authors] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED ([Id] ASC)
);
```

### Book table
```
CREATE TABLE [dbo].[Books] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED ([Id] ASC)
);
```
