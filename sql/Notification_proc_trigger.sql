CREATE OR REPLACE FUNCTION public."NotifyLastLogChange"()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$ 
DECLARE 
  data JSON; notification JSON;
BEGIN	

  IF (TG_OP = 'INSERT')     THEN
	 data = row_to_json(NEW);
  ELSIF (TG_OP = 'UPDATE')  THEN
	 data = row_to_json(NEW);
  ELSIF (TG_OP = 'DELETE')  THEN
	 data = row_to_json(OLD);
  END IF;
  
  notification = json_build_object(
            'table',TG_TABLE_NAME,
            'action', TG_OP,
            'data', data);  
			
   PERFORM pg_notify('lastlogchange', notification::TEXT);
   
  RETURN NEW;
END
$function$
;


ALTER FUNCTION public."NotifyLastLogChange"()
    OWNER TO admin;
    
CREATE OR REPLACE TRIGGER "OnLastLogChange"
    AFTER INSERT OR DELETE OR UPDATE 
    ON public."Tbllogs" 
    FOR EACH ROW
    EXECUTE PROCEDURE public."NotifyLastLogChange"();
