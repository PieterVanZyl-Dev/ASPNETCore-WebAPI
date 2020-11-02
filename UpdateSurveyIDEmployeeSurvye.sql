UPDATE 
     t1
SET 
     t1.EmployeeSurvey = t2.SurveyID 
FROM 
     dbo.Employee t1, 
     dbo.EmployeeSurvey t2 
WHERE 
     t1.EmployeeNumber = t2.EmployeeId;