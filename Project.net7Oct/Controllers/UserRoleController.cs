using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Project.net7Oct.Models; // Adjust namespace if needed

namespace Project.net7Oct.Controllers
{
    [Authorize(Roles = "Admin")]

    public class UserRoleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserRole
        public ActionResult Index()
        {
            var users = db.Users.ToList();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var model = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var roles = userManager.GetRoles(user.Id);
                model.Add(new UserRolesViewModel
                {
                    UserId = user.Id,
                    Email= user.Email,
                    Role = roles.FirstOrDefault()

                });
            }

            return View(model);
        }

        public ActionResult Assign(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Index");

            var user = db.Users.Find(userId);
            if (user == null)
                return RedirectToAction("Index");

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var allRoles = roleManager.Roles.ToList();

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var userRoles = userManager.GetRoles(user.Id);

            var model = new AssignRoleViewModel
            {
                UserId = user.Id,
                SelectedRole = userRoles.FirstOrDefault(),
                Roles = allRoles.Select(r => new RoleSelection
                {
                    RoleName = r.Name,
                    IsSelected = userRoles.Contains(r.Name)
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Assign(AssignRoleViewModel model)
        {
            if (string.IsNullOrEmpty(model.UserId) || string.IsNullOrEmpty(model.SelectedRole))
                return RedirectToAction("Index");

            var user = db.Users.Find(model.UserId);
            if (user == null)
                return RedirectToAction("Index");

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var oldRoles = userManager.GetRoles(user.Id).ToArray();
            if (oldRoles.Any())
                userManager.RemoveFromRoles(user.Id, oldRoles);

            userManager.AddToRole(user.Id, model.SelectedRole);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Index");

            var user = db.Users.Find(userId);
            if (user == null)
                return RedirectToAction("Index");

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roles = userManager.GetRoles(user.Id).ToArray();

            if (roles.Any())
                userManager.RemoveFromRoles(user.Id, roles);

            return RedirectToAction("Index");
        }
    }

    // View Models
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }

    public class AssignRoleViewModel
    {
        public string UserId { get; set; }
        public string SelectedRole { get; set; }
        public List<RoleSelection> Roles { get; set; }
    }

    public class RoleSelection
    {
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}