# icd0021-21-22-s
Viktoria Mihailova (vimihh 206758IADB)

dotnet ef migrations add --project App.DAL.EF --startup-project WebApp --context AppDbContext Initial

dotnet ef migrations remove --project App.DAL.EF --startup-project WebApp --context AppDbContext

dotnet ef database update --project App.DAL.EF --startup-project WebApp --context AppDbContext

dotnet ef database drop --project App.DAL.EF --startup-project WebApp --context AppDbContext



dotnet aspnet-codegenerator controller -name CardsController       -actions -m  App.Domain.Card    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name CordinatesController       -actions -m  App.Domain.Coordinate    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name CouponCategoriesController       -actions -m  App.Domain.CouponCategory    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name ItemCategoriesController       -actions -m  App.Domain.ItemCategory    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name ItemsInOrderController       -actions -m  App.Domain.ItemInOrder    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name MenuItemsController       -actions -m  App.Domain.MenuItem    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name OrderController       -actions -m  App.Domain.Order    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name TicketsController       -actions -m  App.Domain.Ticket    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name TicketsInOrderController       -actions -m  App.Domain.TicketInOrder    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name UsersCategoryController       -actions -m  App.Domain.UserCategory    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name UsersCouponController       -actions -m  App.Domain.UserCoupon    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name UsersInCategoryController       -actions -m  App.Domain.UserInCategory    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name UserLogsController       -actions -m  App.Domain.UserLog    -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f