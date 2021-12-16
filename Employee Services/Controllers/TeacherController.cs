using Employee_Services.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CrudWithOutEntity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        string constr = "Server = LAPTOP-IS8K54U6\\SQLSERVER2019; Database=EmployeeDB; Trusted_Connection = True";
        // GET: api/Teacher
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherModel>>> GetAllTeacher()
        {
            List<TeacherModel> teachers = new List<TeacherModel>();
            string query = "SELECT * FROM Teacher";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            teachers.Add(new TeacherModel
                            {
                                Id = Convert.ToInt32(sdr["Id"]),
                                Teacher_Name = Convert.ToString(sdr["Teacher_Name"]),
                                Teacher_Email = Convert.ToString(sdr["Teacher_Email"]),
                                Teacher_ContactNo = Convert.ToString(sdr["Teacher_ContactNo"]),
                                Teacher_Address = Convert.ToString(sdr["Teacher_Address"]),
                                Teacher_Department = Convert.ToString(sdr["Teacher_Department"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            return teachers;
        }

        // GET: api/Teacher/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherModel>> GetTeacher(long id)
        {

            TeacherModel teacherObj = new TeacherModel();
            string query = "SELECT * FROM Teacher where Id=" + id;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            teacherObj = new TeacherModel
                            {
                                Id = Convert.ToInt32(sdr["Id"]),
                                Teacher_Name = Convert.ToString(sdr["Teacher_Name"]),
                                Teacher_Email = Convert.ToString(sdr["Teacher_Email"]),
                                Teacher_ContactNo = Convert.ToString(sdr["Teacher_ContactNo"]),
                                Teacher_Address = Convert.ToString(sdr["Teacher_Address"]),
                                Teacher_Department = Convert.ToString(sdr["Teacher_Department"])
                            };
                        }
                    }
                    con.Close();
                }
            }
            if (teacherObj == null)
            {
                return NotFound();
            }
            return teacherObj;
        }
        // PUT: api/Teacher/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(long id, TeacherModel teacherModel)
        {
            if (id != teacherModel.Id)
            {
                return BadRequest();
            }
            TeacherModel teacher = new TeacherModel();
            if (ModelState.IsValid)
            {
                string query = "UPDATE Teacher SET Teacher_Name = @Teacher_Name, Teacher_Email = @Teacher_Email," +
                    "Teacher_ContactNo=@Teacher_ContactNo," +
                    "Teacher_Address=@Teacher_Address,Teacher_Department=@Teacher_Department Where Id =@Id";
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Teacher_Name", teacherModel.Teacher_Name);
                        cmd.Parameters.AddWithValue("@Teacher_Email", teacherModel.Teacher_Email);
                        cmd.Parameters.AddWithValue("@Teacher_ContactNo", teacherModel.Teacher_ContactNo);
                        cmd.Parameters.AddWithValue("@Teacher_Address", teacherModel.Teacher_Address);
                        cmd.Parameters.AddWithValue("@Teacher_Department", teacherModel.Teacher_Department);
                        cmd.Parameters.AddWithValue("@Id", teacherModel.Id);
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            return NoContent();
                        }
                        con.Close();
                    }
                }

            }
            return BadRequest(ModelState);
        }

        // POST: api/Teacher
        [HttpPost]
        public async Task<ActionResult<TeacherModel>> PostTeacher(TeacherModel teacherModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (SqlConnection con = new SqlConnection(constr))
            {
                //inserting Patient data into database
                string query = "insert into Teacher values (@Teacher_Name, @Teacher_Email, @Teacher_ContactNo,@Teacher_Address,@Teacher_Department)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Teacher_Name", teacherModel.Teacher_Name);
                    cmd.Parameters.AddWithValue("@Teacher_Email", teacherModel.Teacher_Email);
                    cmd.Parameters.AddWithValue("@Teacher_ContactNo", teacherModel.Teacher_ContactNo);
                    cmd.Parameters.AddWithValue("@Teacher_Address", teacherModel.Teacher_Address);
                    cmd.Parameters.AddWithValue("@Teacher_Department", teacherModel.Teacher_Department);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return Ok(i);
                    }
                    con.Close();
                }
            }
            return BadRequest();

        }

        // DELETE: api/Teacher/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(long id)
        {

            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "Delete FROM Teacher where Id='" + id + "'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return NoContent();
                    }
                    con.Close();
                }
            }
            return BadRequest();
        }

    }
}
