SELECT
   ROW_NUMBER() OVER (ORDER BY  COUNT(*) DESC) AS "Row#",
   REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (gba.description, '0', ''), '1', ''), '2', ''), '3', ''), '4', ''), '5', ''), '6', ''), '7', ''), '8', ''), '9', '') as "Error Message",
   MIN(gba.entry_time) AS "Err Time",
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
   AND gba.entry_time > {fromDate}
   AND gba.entry_time - 1 <= {toDate}
   AND gba.entry_type IN 
   (
      5,
      6
   )
GROUP BY
   REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (gba.description, '0', ''), '1', ''), '2', ''), '3', ''), '4', ''), '5', ''), '6', ''), '7', ''), '8', ''), '9', ''),
   gba.task_id,
   tt.task_name,
   gba.batch_run_num
ORDER BY COUNT(*) DESC