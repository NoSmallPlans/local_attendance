﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;

namespace _5051.Controllers
{
    public class RemoteStudentController : Controller
    {
        // A ViewModel used for the Student that contains the StudentList
        private StudentViewModel StudentViewModel = new StudentViewModel();

        // The Backend Data source
        private StudentBackend StudentBackend = StudentBackend.Instance;
        // GET: /AdminPanel/Options/someName
        public ActionResult Report()
        {
            // Query backend to refresh every time Index() is called
            var myDataList = StudentBackend.Index();
            var StudentViewModel = new StudentViewModel(myDataList);
            var StudentModel = new StudentModel(StudentViewModel.StudentList[0]);

            return View(StudentModel);
        }
        //Return Achievements page
        public ActionResult Achievements()
        {
            return View();
        }
        
        //Returns Avatar select page
        public ActionResult ChooseAvatar()
        {
            
            // Query backend to refresh every time Index() is called
            var myDataList = StudentBackend.Index();
            var StudentViewModel = new StudentViewModel(myDataList);
            var StudentModel = new StudentModel(StudentViewModel.StudentList[0]);
            
            return View(StudentModel);
        }

                         // NOTE: Scott, thanks for the code
        /// <summary>
        /// Choose avatar and update the student model
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: Student/ChooseAvatar
        [HttpPost]
        public ActionResult ChooseAvatar([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "AvatarId,"+
                                        "IsActive,"+
                                        "IsEdit,"+
                                        "LoginStatus,"+
                                        "DailyStatus,"+
                                        "TimeIn,"+
                                        "TimeOut,"+
                                        "Username,"+
                                        "")] StudentModel data)
        {
            
            if (!ModelState.IsValid)
            {
                // Send back for edit
                return RedirectToAction("Valid", new { route = "Home", action = "Error" });
                return View(data);
            }

            if (data == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
                
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Return back for Edit
                return RedirectToAction("NullOrEmpty", new { route = "Home", action = "Error" });
                return View(data);
            }

            // Make it official
            StudentBackend.Update(data);

            return RedirectToAction("Report");
        }


        /// <summary>
        /// Looks up a student in the backend and sends to the view to render 
        /// the student history report.
        /// </summary>
        /// <param name="id"></param> Student ID
        /// <returns></returns>
        // GET: RemoteStudent/StudentHistory/5
        public ActionResult StudentHistory(string id = null)
        {
            // Query our backend backend
            var myDataList = StudentBackend.Index();
            var StudentViewModel = new StudentViewModel(myDataList);

            // Attempt to lookup student if we received an ID
            if (!string.IsNullOrEmpty(id))
            {
                foreach (var item in StudentViewModel.StudentList)
                {
                    if (id == item.Id)
                    {
                        // We found the correct student so send up the model
                        return View(item);
                    }
                }
            }

            // Default to sending up no model
            return View();
        }
    }
}