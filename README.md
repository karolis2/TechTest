## How to run
* **1. Clone the the project.**
* **2. Open Package Manager console, change directory to UserManagement.Data with "cd UserManagement.Data".**
* **3. Run "dotnet ef database update" and SQLITE db will be created locall if it's not yet created.**
* **4. Change starup project to UserManagement.Web.**
* **5. Hit F5 an all the controls will be there as it was orginaly.**
* 

# User Management Technical Exercise

The exercise is an ASP.NET Core web application backed by Entity Framework Core, which faciliates management of some fictional users.
We recommend that you use [Visual Studio (Community Edition)](https://visualstudio.microsoft.com/downloads) or [Visual Studio Code](https://code.visualstudio.com/Download) to run and modify the application. 

**The application uses an in-memory database, so changes will not be persisted between executions.**

## The Exercise
Complete as many of the tasks below as you can. These are split into 3 levels of difficulty 
* **Standard** - Functionality that is common when working as a web developer
* **Advanced** - Slightly more technical tasks and problem solving
* **Expert** - Tasks with a higher level of problem solving and architecture needed


### 1. Filters Section (Standard)

The users page contains 3 buttons below the user listing - **Show All**, **Active Only** and **Non Active**. Show All has already been implemented. Implement the remaining buttons using the following logic:
* Active Only – This should show only users where their `IsActive` property is set to `true`
* Non Active – This should show only users where their `IsActive` property is set to `false`

**COMMENT: Just used 2 buttons and each of them passes true or false and makes a query where user IsActive or not.**

### 2. User Model Properties (Standard)

Add a new property to the `User` class in the system called `DateOfBirth` which is to be used and displayed in relevant sections of the app.

**COMMENT: Simple Date added and desplayed.**

### 3. Actions Section (Standard)

Create the code and UI flows for the following actions
* **Add** – A screen that allows you to create a new user and return to the list
* **View** - A screen that displays the information about a user
* **Edit** – A screen that allows you to edit a selected user from the list  
* **Delete** – A screen that allows you to delete a selected user from the list
Each of these screens should contain appropriate data validation, which is communicated to the end user.


**COMMENT: Simple crud added, I did do it the hard way by creating the each view for the actions, but if I had to do it from scratch, I would have just created controller and let VS generate views for me. Also added some basic validation through the tags and date of birth cannot be later than today. Also tried to add some unit test but did not develop tests for the whole project as it's just an exercise. Also after adding first users, the dropdown is difficult to see, so probably need to fix table size to a minimum size.**

### 4. Data Logging (Advanced)

Extend the system to capture log information regarding primary actions performed on each user in the app.
* In the **View** screen there should be a list of all actions that have been performed against that user. 
* There should be a new **Logs** page, containing a list of log entries across the application.
* In the Logs page, the user should be able to click into each entry to see more detail about it.
* In the Logs page, think about how you can provide a good user experience - even when there are many log entries.

**COMMENT: Here I implemented a BeforeSaveChanges and AfterSaveChanges methods where I used ChangeTracker.DetectChanges(); to track the changes and create audit logs. AfterSaveChanges was needed to create correct logs on user creation as onSave we do not have saved ID. 
Also added a simple partial view which is used fo all of the logs page and for the UserDetailsvView. If I had more time, I would implement Pagination as it should help with the UX. Also did not add any UT for this due to personal time constraints, but would have to test the creation and retrieving of the logs.**

### 5. Extend the Application (Expert)

Make a significant architectural change that improves the application.
Structurally, the user management application is very simple, and there are many ways it can be made more maintainable, scalable or testable.
Some ideas are:
* Re-implement the UI using a client side framework connecting to an API. Use of Blazor is preferred, but if you are more familiar with other frameworks, feel free to use them.
* Update the data access layer to support asynchronous operations.
* Implement authentication and login based on the users being stored.
* Implement bundling of static assets.
* Update the data access layer to use a real database, and implement database schema migrations.

**COMMENT: Here I only added SQLite and migrations to use the db. But I spent to much time with CRUD. But for data layer I would async awayt and async functions.
for the login username and pwd would have to be checked on the DB and details would have to stay in the session, so we could use for the audit logs. Also as data layer is kept in the services, api would just be accessing the them.**

## Additional Notes

* Please feel free to change or refactor any code that has been supplied within the solution and think about clean maintainable code and architecture when extending the project.
* If any additional packages, tools or setup are required to run your completed version, please document these thoroughly.
