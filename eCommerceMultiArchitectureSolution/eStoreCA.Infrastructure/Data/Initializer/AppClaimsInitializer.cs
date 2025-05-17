
using eStoreCA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eStoreCA.Infrastructure.Data.Initializer
{
    public class AppClaimsInitializer
    {
        public static void AppClaimsAsync(ApplicationDbContext db)
        {
            List<AppClaim> appClaimsList = new List<AppClaim>();


            #region Roles
            AppClaim claimRole = new AppClaim();

            claimRole.Id = Guid.NewGuid();
            claimRole.AppClaims = new List<AppClaim>();
            claimRole.ClaimTitle = "Role";
            claimRole.DisplayName = "Role";
            claimRole.ParentId = null;
            claimRole.UrlLink = "";
            claimRole.DisplayOrder = 1;
            claimRole.IsActive = true;
            appClaimsList.Add(claimRole);


            AppClaim claimRoleList = new AppClaim();

            claimRoleList.Id = Guid.NewGuid();

            claimRoleList.DisplayName = "List";
            claimRoleList.ClaimTitle = "Permissions.Role.List";
            claimRoleList.ParentId = claimRole.Id;
            claimRoleList.UrlLink = "";
            claimRoleList.DisplayOrder = 1;
            claimRoleList.IsActive = true;
            claimRole.AppClaims.Add(claimRoleList);


            AppClaim claimRoleView = new AppClaim();


            claimRoleView.Id = Guid.NewGuid();

            claimRoleView.DisplayName = "View";
            claimRoleView.ClaimTitle = "Permissions.Role.View";
            claimRoleView.ParentId = claimRole.Id;
            claimRoleView.UrlLink = "";
            claimRoleView.DisplayOrder = 2;
            claimRoleView.IsActive = true;
            claimRole.AppClaims.Add(claimRoleView);


            AppClaim claimRoleCreate = new AppClaim();


            claimRoleCreate.Id = Guid.NewGuid();


            claimRoleCreate.DisplayName = "Create";
            claimRoleCreate.ClaimTitle = "Permissions.Role.Create";
            claimRoleCreate.ParentId = claimRole.Id;
            claimRoleCreate.UrlLink = "";
            claimRoleCreate.DisplayOrder = 3;
            claimRoleCreate.IsActive = true;
            claimRole.AppClaims.Add(claimRoleCreate);


            AppClaim claimRoleEdit = new AppClaim();


            claimRoleEdit.Id = Guid.NewGuid();


            claimRoleEdit.DisplayName = "Edit";
            claimRoleEdit.ClaimTitle = "Permissions.Role.Edit";
            claimRoleEdit.ParentId = claimRole.Id;
            claimRoleEdit.UrlLink = "";
            claimRoleEdit.DisplayOrder = 4;
            claimRoleEdit.IsActive = true;
            claimRole.AppClaims.Add(claimRoleEdit);


            AppClaim claimRoleDelete = new AppClaim();

            claimRoleDelete.Id = Guid.NewGuid();

            claimRoleDelete.DisplayName = "Delete";
            claimRoleDelete.ClaimTitle = "Permissions.Role.Delete";
            claimRoleDelete.ParentId = claimRole.Id;
            claimRoleDelete.UrlLink = "";
            claimRoleDelete.DisplayOrder = 5;
            claimRoleDelete.IsActive = true;
            claimRole.AppClaims.Add(claimRoleDelete);

            #endregion

            #region Users
            AppClaim claimUser = new AppClaim();


            claimUser.Id = Guid.NewGuid();


            claimUser.AppClaims = new List<AppClaim>();
            claimUser.ClaimTitle = "User";
            claimUser.DisplayName = "User";
            claimUser.ParentId = null;
            claimUser.UrlLink = "";
            claimUser.DisplayOrder = 1;
            claimUser.IsActive = true;
            appClaimsList.Add(claimUser);


            AppClaim claimUserList = new AppClaim();


            claimUserList.Id = Guid.NewGuid();


            claimUserList.DisplayName = "List";
            claimUserList.ClaimTitle = "Permissions.User.List";
            claimUserList.ParentId = claimUser.Id;
            claimUserList.UrlLink = "";
            claimUserList.DisplayOrder = 1;
            claimUserList.IsActive = true;
            claimUser.AppClaims.Add(claimUserList);


            AppClaim claimUserView = new AppClaim();


            claimUserView.Id = Guid.NewGuid();

            claimUserView.DisplayName = "View";
            claimUserView.ClaimTitle = "Permissions.User.View";
            claimUserView.ParentId = claimUser.Id;
            claimUserView.UrlLink = "";
            claimUserView.DisplayOrder = 2;
            claimUserView.IsActive = true;
            claimUser.AppClaims.Add(claimUserView);


            AppClaim claimUserCreate = new AppClaim();



            claimUserCreate.Id = Guid.NewGuid();


            claimUserCreate.DisplayName = "Create";
            claimUserCreate.ClaimTitle = "Permissions.User.Create";
            claimUserCreate.ParentId = claimUser.Id;
            claimUserCreate.UrlLink = "";
            claimUserCreate.DisplayOrder = 3;
            claimUserCreate.IsActive = true;
            claimUser.AppClaims.Add(claimUserCreate);


            AppClaim claimUserEdit = new AppClaim();


            claimUserEdit.Id = Guid.NewGuid();

            claimUserEdit.DisplayName = "Edit";
            claimUserEdit.ClaimTitle = "Permissions.User.Edit";
            claimUserEdit.ParentId = claimUser.Id;
            claimUserEdit.UrlLink = "";
            claimUserEdit.DisplayOrder = 4;
            claimUserEdit.IsActive = true;
            claimUser.AppClaims.Add(claimUserEdit);


            AppClaim claimUserDelete = new AppClaim();

            claimUserDelete.Id = Guid.NewGuid();

            claimUserDelete.DisplayName = "Delete";
            claimUserDelete.ClaimTitle = "Permissions.User.Delete";
            claimUserDelete.ParentId = claimUser.Id;
            claimUserDelete.UrlLink = "";
            claimUserDelete.DisplayOrder = 5;
            claimUserDelete.IsActive = true;
            claimUser.AppClaims.Add(claimUserDelete);

            #endregion





            #region Category
            AppClaim claimCategory = new AppClaim();
            claimCategory.Id = Guid.NewGuid();
            claimCategory.AppClaims = new List<AppClaim>();
            claimCategory.ClaimTitle = "Category";
            claimCategory.DisplayName = "Category";
            claimCategory.ParentId = null;
            claimCategory.UrlLink = "";
            claimCategory.DisplayOrder = 1;
            claimCategory.IsActive = true;
            appClaimsList.Add(claimCategory);


            AppClaim claimCategoryList = new AppClaim();
            claimCategoryList.Id = Guid.NewGuid();
            claimCategoryList.DisplayName = "List";
            claimCategoryList.ClaimTitle = "Permissions.Category.List";
            claimCategoryList.ParentId = claimCategory.Id;
            claimCategoryList.UrlLink = "";
            claimCategoryList.DisplayOrder = 1;
            claimCategoryList.IsActive = true;
            claimCategory.AppClaims.Add(claimCategoryList);


            AppClaim claimCategoryView = new AppClaim();
            claimCategoryView.Id = Guid.NewGuid();
            claimCategoryView.DisplayName = "View";
            claimCategoryView.ClaimTitle = "Permissions.Category.View";
            claimCategoryView.ParentId = claimCategory.Id;
            claimCategoryView.UrlLink = "";
            claimCategoryView.DisplayOrder = 2;
            claimCategoryView.IsActive = true;
            claimCategory.AppClaims.Add(claimCategoryView);


            AppClaim claimCategoryCreate = new AppClaim();
            claimCategoryCreate.Id = Guid.NewGuid();
            claimCategoryCreate.DisplayName = "Create";
            claimCategoryCreate.ClaimTitle = "Permissions.Category.Create";
            claimCategoryCreate.ParentId = claimCategory.Id;
            claimCategoryCreate.UrlLink = "";
            claimCategoryCreate.DisplayOrder = 3;
            claimCategoryCreate.IsActive = true;
            claimCategory.AppClaims.Add(claimCategoryCreate);


            AppClaim claimCategoryEdit = new AppClaim();
            claimCategoryEdit.Id = Guid.NewGuid();
            claimCategoryEdit.DisplayName = "Edit";
            claimCategoryEdit.ClaimTitle = "Permissions.Category.Edit";
            claimCategoryEdit.ParentId = claimCategory.Id;
            claimCategoryEdit.UrlLink = "";
            claimCategoryEdit.DisplayOrder = 4;
            claimCategoryEdit.IsActive = true;
            claimCategory.AppClaims.Add(claimCategoryEdit);


            AppClaim claimCategoryDelete = new AppClaim();
            claimCategoryDelete.Id = Guid.NewGuid();
            claimCategoryDelete.DisplayName = "Delete";
            claimCategoryDelete.ClaimTitle = "Permissions.Category.Delete";
            claimCategoryDelete.ParentId = claimCategory.Id;
            claimCategoryDelete.UrlLink = "";
            claimCategoryDelete.DisplayOrder = 5;
            claimCategoryDelete.IsActive = true;
            claimCategory.AppClaims.Add(claimCategoryDelete);
            #endregion




            #region Custom
            #endregion Custom

            List<AppClaim> existsAppClaims = new List<AppClaim>();

            existsAppClaims = db.AppClaims.ToListAsync().GetAwaiter().GetResult();

            foreach (var apc in appClaimsList)
            {
                if (!existsAppClaims.Any(u => u.ClaimTitle.ToUpper() == apc.ClaimTitle.ToUpper()))
                {
                    db.AppClaims.AddAsync(apc).GetAwaiter().GetResult();
                }
            }

            db.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
