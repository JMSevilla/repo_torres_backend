using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TORRES_backend.Helpers.v2;
using TORRES_backend.Models;

namespace TORRES_backend.Controllers.v2.Employee
{
    [RoutePrefix("api/v2/employee")]
    public class employeeUsersController : ApiController
    {
        private ttcdbEntities db = new ttcdbEntities();

        // GET: api/employeeUsers
        public IQueryable<employeeUser> GetemployeeUsers()
        {
            return db.employeeUsers;
        }

        // GET: api/employeeUsers/5
        [ResponseType(typeof(employeeUser))]
        public async Task<IHttpActionResult> GetemployeeUser(int id)
        {
            employeeUser employeeUser = await db.employeeUsers.FindAsync(id);
            if (employeeUser == null)
            {
                return NotFound();
            }

            return Ok(employeeUser);
        }

        // PUT: api/employeeUsers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutemployeeUser(int id, employeeUser employeeUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeUser.empID)
            {
                return BadRequest();
            }

            db.Entry(employeeUser).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!employeeUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        employeeHelper __helper = new employeeHelper();
        // POST: api/employeeUsers
        [Route("add-employee"), HttpPost]
        public async Task<IHttpActionResult> PostemployeeUser()
        {
            
            try
            {
                IHttpActionResult result = null;
                __helper.employeeAdding();
                result = Ok(employeeHelper.response);
                return await Task.FromResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE: api/employeeUsers/5
        [ResponseType(typeof(employeeUser))]
        public async Task<IHttpActionResult> DeleteemployeeUser(int id)
        {
            employeeUser employeeUser = await db.employeeUsers.FindAsync(id);
            if (employeeUser == null)
            {
                return NotFound();
            }

            db.employeeUsers.Remove(employeeUser);
            await db.SaveChangesAsync();

            return Ok(employeeUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool employeeUserExists(int id)
        {
            return db.employeeUsers.Count(e => e.empID == id) > 0;
        }
    }
}