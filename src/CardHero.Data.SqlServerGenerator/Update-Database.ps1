﻿Scaffold-DbContext `
"Server=.\SQLDEV2016;Database=CardHero;Integrated Security=true;MultipleActiveResultSets=true" `
Microsoft.EntityFrameworkCore.SqlServer `
-Force `
-Project CardHero.Data.SqlServer `
-StartupProject CardHero.Data.SqlServerGenerator `
-Context CardHeroDataDbContext `
-OutputDir EntityFramework `
-Tables `
Game,`
GameUser,`
Move,`
Turn
