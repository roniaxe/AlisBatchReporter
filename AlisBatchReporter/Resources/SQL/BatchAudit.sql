SELECT
   ROW_NUMBER() OVER (ORDER BY  COUNT(*) DESC) AS "Row#",
   REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (gba.description, '0', ''), '1', ''), '2', ''), '3', ''), '4', ''), '5', ''), '6', ''), '7', ''), '8', ''), '9', '') as "Error Message",
   MIN(gba.entry_time) AS "Err Time",
   CASE WHEN (SELECT count(gba2.entry_type) 
			   FROM g_batch_audit gba2 
			   WHERE gba2.batch_run_num = gba.batch_run_num 
			   AND ((gba2.entry_type = 4 AND gba2.description LIKE '%terminated%')
			   OR (gba2.entry_type = 2 and gba2.description LIKE '%End of batch run%'))) > 0 
			   THEN 'Finished' 
			   ELSE 'Did Not Finish' end as "Status",
   gba.task_id AS "Task ID",
   tt.task_name AS "Task",
   gba.batch_run_num AS "Batch Run Number",
   SUM(
	  CASE
	    WHEN entry_type = 5
	    THEN 1
	    ELSE 0
	  END) AS "Tech Errors",
	  SUM(
	  CASE
	    WHEN entry_type = 6
	    THEN 1
	    ELSE 0
	  END)     AS "App Errors" ,
	  SUM(
	  CASE
	    WHEN entry_type = 4
	    THEN 1
	    ELSE 0
	  END)     AS "Warnings" ,
	  COUNT(*) AS "Total"
FROM
   g_batch_audit gba JOIN t_task tt
   ON gba.task_id = tt.task_id
WHERE
   1=1
   AND CONVERT(DATE, gba.ENTRY_TIME+1) > {Param1}
   AND CONVERT(DATE, gba.ENTRY_TIME) <= {Param2}
   {Param4}
   {Param3}
GROUP BY
   REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (gba.description, '0', ''), '1', ''), '2', ''), '3', ''), '4', ''), '5', ''), '6', ''), '7', ''), '8', ''), '9', ''),
   gba.task_id,
   tt.task_name,
   gba.batch_run_num
ORDER BY COUNT(*) DESC
option(fast 100)