<?php
/**
 * Locale
 *
 * @package ebc
 * @subpackage ebc.app.locale
 */
$SBS_LANG = array();

# Login
$SBS_LANG['login_username'] = "Username";
$SBS_LANG['login_password'] = "Password";
$SBS_LANG['login_login']    = "Admin Login";
$SBS_LANG['login_register'] = "Register";
$SBS_LANG['login_err'][1] = "Wrong username or password";
$SBS_LANG['login_err'][2] = "Access denied";
$SBS_LANG['login_err'][3] = "Account is disabled";
$SBS_LANG['login_error'] = "Error";

# Left menu
$SBS_LANG['menu_calendar']   = "Calendar";
$SBS_LANG['menu_bookings']   = "Bookings";
$SBS_LANG['menu_services'] = "Services";
$SBS_LANG['menu_options'] = "Options";
$SBS_LANG['menu_install'] = "Install";
$SBS_LANG['menu_preview'] = "Preview";
$SBS_LANG['menu_logout']  = "Logout";
$SBS_LANG['menu_employees'] = "Employees";
$SBS_LANG['menu_users']   = "Users";
$SBS_LANG['menu_time'] = "Working Time";

$SBS_LANG['menu_choose_calendar'] = "-- Choose calendar --";

# Calendar
$SBS_LANG['calendar_add'] = "Add calendar";
$SBS_LANG['calendar_title']  = "Calendar";
$SBS_LANG['calendar_update'] = "Update calendar";
$SBS_LANG['calendar_create'] = "Add calendar";
$SBS_LANG['calendar_list']   = "Calendars";
$SBS_LANG['calendar_owner']  = "Owner";
$SBS_LANG['calendar_choose'] = "-- Choose --";
$SBS_LANG['calendar_empty'] = "No calendars found";
$SBS_LANG['calendar_del_title'] = "Delete this calendar?";
$SBS_LANG['calendar_del_body'] = "All calendar' details will be deleted and will not be possible to restore them.";

$SBS_LANG['calendar_err'][1] = "Calendar has been added";
$SBS_LANG['calendar_err'][2] = "Calendar has not been added";
$SBS_LANG['calendar_err'][3] = "Calendar has been deleted";
$SBS_LANG['calendar_err'][4] = "Calendar has not been deleted";
$SBS_LANG['calendar_err'][5] = "Calendar has been updated";
$SBS_LANG['calendar_err'][6] = "Calendar has not been updated";
$SBS_LANG['calendar_err'][7] = "";
$SBS_LANG['calendar_err'][8] = "Calendar doesn't exists";
$SBS_LANG['calendar_err'][9] = "Calendar doesn't belongs to you";
$SBS_LANG['calendar_err'][10] = "Day color saved";

# Working time
$SBS_LANG['time_update'] = "Update";
$SBS_LANG['time_default'] = "Default";
$SBS_LANG['time_custom'] = "Custom";
$SBS_LANG['time_date'] = "Date";
$SBS_LANG['time_length'] = "Slot length";
$SBS_LANG['time_limit'] = "Bookings per slot";
$SBS_LANG['time_slot'] = "Slot length";
$SBS_LANG['time_del_title'] = "Delete this date?";
$SBS_LANG['time_del_body'] = "All date details will be deleted and will not be possible to restore them.";
$SBS_LANG['time_empty'] = "No custom dates found";

$SBS_LANG['time_title'] = "Working Time";
$SBS_LANG['time_from'] = "Start Time";
$SBS_LANG['time_to'] = "End Time";
$SBS_LANG['time_is'] = "Is Day off";
$SBS_LANG['time_day'] = "Day of week";
$SBS_LANG['time_price'] = "Price";
$SBS_LANG['time_single_price'] = "Use the same price for all slots during the day";
$SBS_LANG['time_err_4'] = "Working Time has been updated successfully";

$SBS_LANG['time_dp_title'] = "Set custom prices";

# Service
$SBS_LANG['service_add'] = "Add service";
$SBS_LANG['service_title']  = "Service";
$SBS_LANG['service_update'] = "Update service";
$SBS_LANG['service_create'] = "Add service";
$SBS_LANG['service_list']   = "Services";
$SBS_LANG['service_description'] = "Service description";
$SBS_LANG['service_price']   = "Price";
$SBS_LANG['service_length'] = "Length (minutes)";
$SBS_LANG['service_after']  = "After (minutes)";
$SBS_LANG['service_before'] = "Before (minutes)";
$SBS_LANG['service_total']  = "Total (minutes)";
$SBS_LANG['service_choose'] = "-- Choose --";
$SBS_LANG['service_empty']  = "No services found";
$SBS_LANG['service_del_title'] = "Delete this service?";
$SBS_LANG['service_del_body'] = "All service' details will be deleted and will not be possible to restore them.";

$SBS_LANG['service_err'][1] = "Service has been added";
$SBS_LANG['service_err'][2] = "Service has not been added";
$SBS_LANG['service_err'][3] = "Service has been deleted";
$SBS_LANG['service_err'][4] = "Service has not been deleted";
$SBS_LANG['service_err'][5] = "Service has been updated";
$SBS_LANG['service_err'][6] = "Service has not been updated";
$SBS_LANG['service_err'][7] = "";
$SBS_LANG['service_err'][8] = "Service doesn't exists";

# Booking
$SBS_LANG['booking_add'] = "Add booking";
$SBS_LANG['booking_find'] = "Find";
$SBS_LANG['booking_title']  = "Booking";
$SBS_LANG['booking_service']  = "Service";
$SBS_LANG['booking_export']  = "Export";
$SBS_LANG['booking_update'] = "Update booking";
$SBS_LANG['booking_create'] = "Add booking";
$SBS_LANG['booking_list']   = "Bookings";
$SBS_LANG['booking_schedule'] = "Daily Schedule";
$SBS_LANG['booking_q'] = "Keyword";
$SBS_LANG['booking_choose'] = "-- Choose --";
$SBS_LANG['booking_empty'] = "No bookings found";
$SBS_LANG['booking_email_title'] = "Re-send email";
$SBS_LANG['booking_del_title'] = "Delete this booking?";
$SBS_LANG['booking_del_body'] = "All booking details will be deleted and will not be possible to restore them.";
$SBS_LANG['booking_s_title'] = "System notification";
$SBS_LANG['booking_s_body'] = "Please wait while we redirect your browser to the bookings list...";
$SBS_LANG['booking_customer_name'] = "Customer name";
$SBS_LANG['booking_customer_email'] = "Customer e-mail";
$SBS_LANG['booking_customer_phone'] = "Customer phone";
$SBS_LANG['booking_customer_country'] = "Customer country";
$SBS_LANG['booking_customer_city'] = "Customer city";
$SBS_LANG['booking_customer_state'] = "Customer state";
$SBS_LANG['booking_customer_zip'] = "Customer zip";
$SBS_LANG['booking_customer_address'] = "Customer address";
$SBS_LANG['booking_customer_notes'] = "Customer notes";
$SBS_LANG['booking_time'] = "Booking Date/Time";
$SBS_LANG['booking_from'] = "Date from";
$SBS_LANG['booking_to'] = "Date to";
$SBS_LANG['booking_total'] = "Total";
$SBS_LANG['booking_deposit'] = "Deposit";
$SBS_LANG['booking_tax'] = "Tax";
$SBS_LANG['booking_date'] = "Booking date";
$SBS_LANG['booking_dayoff'] = "Day off";
$SBS_LANG['booking_cc_type'] = 'Credit Card type';
$SBS_LANG['booking_cc_num'] = 'Credit Card number';
$SBS_LANG['booking_cc_exp'] = 'Credit Card expiration date';
$SBS_LANG['booking_cc_code'] = 'Credit Card security code';
$SBS_LANG['booking_employee'] = "Employee";
$SBS_LANG['booking_start'] = "Start time";
$SBS_LANG['booking_price'] = "Price";
$SBS_LANG['booking_remove'] = "Remove";

$SBS_LANG['booking_b_del_title'] = "Delete confirmation";
$SBS_LANG['booking_b_del_body'] = "Are you sure you want to remove selected service from this booking?";

$SBS_LANG['booking_option'] = "Customer choose to pay";
$SBS_LANG['booking_options']['total'] = "Total";
$SBS_LANG['booking_options']['deposit'] = "Deposit";

$SBS_LANG['booking_booking_status'] = "Booking status";
$SBS_LANG['booking_booking_statuses']['confirmed'] = "Confirmed";
$SBS_LANG['booking_booking_statuses']['pending'] = "Pending";
$SBS_LANG['booking_booking_statuses']['cancelled'] = "Cancelled";

$SBS_LANG['booking_payment_method'] = "Payment method";
$SBS_LANG['booking_payment_methods']['paypal'] = "PayPal";
$SBS_LANG['booking_payment_methods']['authorize'] = "Authorize.net";
$SBS_LANG['booking_payment_methods']['creditcard'] = "Credit card";

$SBS_LANG['booking_export_from'] = "Date from";
$SBS_LANG['booking_export_to'] = "Date to";
$SBS_LANG['booking_export_calendar'] = "Calendar";
$SBS_LANG['booking_export_all'] = "-- All --";
$SBS_LANG['booking_export_format'] = "Format";

$SBS_LANG['booking_export_note_csv'] = '<abbr class="bold" title="Comma-Separated Values">CSV</abbr> stands for Comma-Separated Values, sometimes also called Comma Delimited. A CSV file is a specially formatted plain text file which stores spreadsheet or basic database-style information in a very simple format, with one record on each line, and each field within that record separated by a comma.';
$SBS_LANG['booking_export_note_xml'] = '<abbr class="bold" title="Extensible Markup Language">XML</abbr> is a standard, simple, self-describing way of encoding both text and data so that content can be processed with relatively little human intervention and exchanged acros diverse hardware, operating systems, and applications. XML is nothing special. It is just plain text. Software that can handle plain text can also handle XML. However, XML-aware applications can handle the XML tags specially.';
$SBS_LANG['booking_export_note_ical'] = '<abbr class="bold" title="">iCalendar</abbr> or iCal, format is a way of formatting calendar information for easy exchange between platforms and applications. It is a standard text file format for calendar files. iCalendar allows users to share event and meeting information via email and on web sites.';

$SBS_LANG['booking_export_map'] = array();
$SBS_LANG['booking_export_map']['calendar_id'] = "Calendar ID";
$SBS_LANG['booking_export_map']['booking_total'] = "Total";
$SBS_LANG['booking_export_map']['booking_deposit'] = "Deposit";
$SBS_LANG['booking_export_map']['booking_tax'] = "Tax";
$SBS_LANG['booking_export_map']['booking_status'] = "Status";
$SBS_LANG['booking_export_map']['payment_method'] = "Payment Method";
$SBS_LANG['booking_export_map']['payment_option'] = "Payment Option";
$SBS_LANG['booking_export_map']['customer_name'] = "Name";
$SBS_LANG['booking_export_map']['customer_email'] = "Email";
$SBS_LANG['booking_export_map']['customer_phone'] = "Phone";
$SBS_LANG['booking_export_map']['customer_country'] = "Country";
$SBS_LANG['booking_export_map']['customer_city'] = "City";
$SBS_LANG['booking_export_map']['customer_state'] = "State";
$SBS_LANG['booking_export_map']['customer_zip'] = "Zip";
$SBS_LANG['booking_export_map']['customer_address'] = "Address";
$SBS_LANG['booking_export_map']['customer_notes'] = "Notes";
$SBS_LANG['booking_export_map']['cc_type'] = "Credit Card Type";
$SBS_LANG['booking_export_map']['cc_num'] = "Credit Card Number";
$SBS_LANG['booking_export_map']['cc_exp'] = "Credit Card Expiration date";
$SBS_LANG['booking_export_map']['cc_code'] = "Credit Card Security Code";
$SBS_LANG['booking_export_map']['txn_id'] = "Txn ID";
$SBS_LANG['booking_export_map']['processed_on'] = "Processed On";

$SBS_LANG['booking_export_map']['id'] = "ID";
$SBS_LANG['booking_export_map']['booking_id'] = "Booking ID";
$SBS_LANG['booking_export_map']['service_id'] = "Service";
$SBS_LANG['booking_export_map']['employee_id'] = "Employee";
$SBS_LANG['booking_export_map']['date'] = "Booking Date";
$SBS_LANG['booking_export_map']['start'] = "Start Time";
$SBS_LANG['booking_export_map']['total'] = "Total minutes";
$SBS_LANG['booking_export_map']['price'] = "Price";

$SBS_LANG['booking_detail_price'] = "Price";
$SBS_LANG['booking_detail_cnt'] = "Count";
$SBS_LANG['booking_detail_amount'] = "Amount";
$SBS_LANG['booking_detail_sub_total'] = "Sub Total";
$SBS_LANG['booking_detail_grand_total'] = "Grand Total";
$SBS_LANG['booking_detail_tax'] = "Tax";
$SBS_LANG['booking_detail_title'] = "Title";
$SBS_LANG['booking_detail_fully'] = "Fully booked";

$SBS_LANG['booking_err'][1] = "Booking has been added";
$SBS_LANG['booking_err'][2] = "Booking has not been added";
$SBS_LANG['booking_err'][3] = "Booking has been deleted";
$SBS_LANG['booking_err'][4] = "Booking has not been deleted";
$SBS_LANG['booking_err'][5] = "Booking has been updated";
$SBS_LANG['booking_err'][6] = "Booking has not been updated";
$SBS_LANG['booking_err'][7] = "";
$SBS_LANG['booking_err'][8] = "Booking doesn't exists";
$SBS_LANG['booking_err'][9] = "Booking doesn't belongs to you";

$SBS_LANG['booking_validation']['valid_price'] = "Please, select at least 1 ticket";
$SBS_LANG['booking_validation']['valid_num'] = "You just reach the limit of available tickets";

# Users
$SBS_LANG['user_update'] = "Update user";
$SBS_LANG['user_create'] = "Add user";
$SBS_LANG['user_list'] = "User list";
$SBS_LANG['user_username'] = "Username";
$SBS_LANG['user_password'] = "Password";
$SBS_LANG['user_role'] = "Role";
$SBS_LANG['user_choose'] = "-- Choose --";
$SBS_LANG['user_status'] = "Status";
$SBS_LANG['user_statarr']['T'] = "Active";
$SBS_LANG['user_statarr']['F'] = "Inactive";
$SBS_LANG['user_empty'] = "No users found";
$SBS_LANG['user_del_title'] = "Delete this user?";
$SBS_LANG['user_del_body'] = "All user' details will be deleted and will not be possible to restore them.";

$SBS_LANG['user_err'][1] = "User has been added";
$SBS_LANG['user_err'][2] = "User has not been added";
$SBS_LANG['user_err'][3] = "User has been deleted";
$SBS_LANG['user_err'][4] = "User has not been deleted";
$SBS_LANG['user_err'][5] = "User has been updated";
$SBS_LANG['user_err'][6] = "User has not been updated";
$SBS_LANG['user_err'][7] = "";
$SBS_LANG['user_err'][8] = "User doesn't exists";

# Employees
$SBS_LANG['employee_update'] = "Update employee";
$SBS_LANG['employee_create'] = "Add employee";
$SBS_LANG['employee_list'] = "Employee list";
$SBS_LANG['employee_name'] = "Employee Name";
$SBS_LANG['employee_email'] = "Email";
$SBS_LANG['employee_phone'] = "Phone";
$SBS_LANG['employee_notes'] = "Notes";
$SBS_LANG['employee_send_email'] = "Send email when new booking is made";
$SBS_LANG['employee_services'] = "Services";
$SBS_LANG['employee_empty'] = "No employees found";
$SBS_LANG['employee_del_title'] = "Delete this employee?";
$SBS_LANG['employee_del_body'] = "All employee' details will be deleted and will not be possible to restore them.";

$SBS_LANG['employee_err'][1] = "Employee has been added";
$SBS_LANG['employee_err'][2] = "Employee has not been added";
$SBS_LANG['employee_err'][3] = "Employee has been deleted";
$SBS_LANG['employee_err'][4] = "Employee has not been deleted";
$SBS_LANG['employee_err'][5] = "Employee has been updated";
$SBS_LANG['employee_err'][6] = "Employee has not been updated";
$SBS_LANG['employee_err'][7] = "";
$SBS_LANG['employee_err'][8] = "Employee doesn't exists";

# Options
$SBS_LANG['option_list'] = "Option list";
$SBS_LANG['option_key'] = "Option key";
$SBS_LANG['option_description'] = "Options";
$SBS_LANG['option_value'] = "Value";
$SBS_LANG['option_install'] = "Install";
$SBS_LANG['option_user_api'] = "User API";
$SBS_LANG['option_general'] = "General";
$SBS_LANG['option_appearance'] = "Appearance";
$SBS_LANG['option_bookings'] = "Bookings";
$SBS_LANG['option_confirmation'] = "Confirmation";
$SBS_LANG['option_booking_form'] = "Booking Form";
$SBS_LANG['option_reminder'] = "Reminder";
$SBS_LANG['option_username'] = "Username";
$SBS_LANG['option_password'] = "Password";
$SBS_LANG['option_hours_before'] = "hours before";
$SBS_LANG['option_get_key'] = "get key";
$SBS_LANG['option_cron'] = "Cron script";
$SBS_LANG['option_cron_info'] = "You need to set up a cron job using your hosting account control panel which should execute every hour. Depending on your web server you should use either the URL or script path.
<br /><br/>
Server path:<br /><span class=\"bold\">%1\$s</span>
<br /><br />
URL:<br /><span class=\"bold\">%2\$s</span>";
$SBS_LANG['option_paypal'] = "PayPal IPN";
$SBS_LANG['option_paypal_info'] = "If payment confirmation does not work you may need to enable IPN and set this URL. <a href='https://cms.paypal.com/us/cgi-bin/?cmd=_render-content&content_ID=developer/e_howto_admin_IPNSetup' target='_blank'>See how</a>.<br /><br />
URL:<br /><span class=\"bold\">%1\$s</span>";

$SBS_LANG['option_err'][5] = "Options has been updated";

$SBS_LANG['o_install_js'] = "Using JS code";
$SBS_LANG['o_install']['js'][1] = "Step 1. (Required) Copy the code below and put it in your HEAD tag.";
$SBS_LANG['o_install']['js'][2] = "Step 2. (Required) Copy the code below and put it in your web page where you want the calendar to appear.";

$SBS_LANG['o_install']['user_api'] = "(Required) Copy the code below and put it in your php file where you want call the function create user and calendar for user.";

# General
$SBS_LANG['_yesno']['T'] = "Yes";
$SBS_LANG['_yesno']['F'] = "No";

$SBS_LANG['_switch'] = "Switch On/Off";
$SBS_LANG['_search'] = "Search";
$SBS_LANG['_save']   = "Save changes";
$SBS_LANG['_cancel'] = "Cancel";
$SBS_LANG['_upload'] = "Upload";
$SBS_LANG['_edit']   = "edit";
$SBS_LANG['_delete'] = "delete";
$SBS_LANG['_delete_all'] = "delete all";
$SBS_LANG['_view']   = "view";
$SBS_LANG['_back']   = "back";
$SBS_LANG['_never']  = "never";
$SBS_LANG['_empty']  = "No records.";
$SBS_LANG['_sure']   = "Are you sure you want to delete selected record?";
$SBS_LANG['_up']     = "up";
$SBS_LANG['_down']   = "down";

$SBS_LANG['status'] = array(
	1 => 'You are not loged in.',
	2 => 'Access denied. You have not requisite rights to.',
	3 => 'Empty resultset.',
	4 => 'The operation has not been successful',
	7 => 'The operation is not allowed in demo mode.',
	8 => 'The operation is allowed only in Multi-calendar mode',
	9 => 'The operation is allowed only in Multi-user mode',
	20 => 'The operation has been successful',
	123 => "Your hosting account does not allow uploading such a large image."
);
$SBS_LANG['err'] = array(
	1 => 'Operation has not been successful.'
);

$SBS_LANG['month_name'][1] = "January";
$SBS_LANG['month_name'][2] = "February";
$SBS_LANG['month_name'][3] = "March";
$SBS_LANG['month_name'][4] = "April";
$SBS_LANG['month_name'][5] = "May";
$SBS_LANG['month_name'][6] = "June";
$SBS_LANG['month_name'][7] = "July";
$SBS_LANG['month_name'][8] = "August";
$SBS_LANG['month_name'][9] = "September";
$SBS_LANG['month_name'][10] = "October";
$SBS_LANG['month_name'][11] = "November";
$SBS_LANG['month_name'][12] = "December";

$SBS_LANG['weekday_name'][0] = "Sunday";
$SBS_LANG['weekday_name'][1] = "Monday";
$SBS_LANG['weekday_name'][2] = "Tuesday";
$SBS_LANG['weekday_name'][3] = "Wednesday";
$SBS_LANG['weekday_name'][4] = "Thursday";
$SBS_LANG['weekday_name'][5] = "Friday";
$SBS_LANG['weekday_name'][6] = "Saturday";

# Days
$SBS_LANG['days']['monday']    = "Monday";
$SBS_LANG['days']['tuesday']   = "Tuesday";
$SBS_LANG['days']['wednesday'] = "Wednesday";
$SBS_LANG['days']['thursday']  = "Thursday";
$SBS_LANG['days']['friday']    = "Friday";
$SBS_LANG['days']['saturday']  = "Saturday";
$SBS_LANG['days']['sunday']    = "Sunday";
$SBS_LANG['days_short']['monday']    = "Mon";
$SBS_LANG['days_short']['tuesday']   = "Tue";
$SBS_LANG['days_short']['wednesday'] = "Wed";
$SBS_LANG['days_short']['thursday']  = "Thu";
$SBS_LANG['days_short']['friday']    = "Fri";
$SBS_LANG['days_short']['saturday']  = "Sat";
$SBS_LANG['days_short']['sunday']    = "Sun";

$SBS_LANG['prev_link'] = "&lt;";
$SBS_LANG['next_link'] = "&gt;";
$SBS_LANG['prev_title'] = "Previous";
$SBS_LANG['next_title'] = "Next";

# FRONT-END
$SBS_LANG['legend_with'] = "Services";
$SBS_LANG['legend_without'] = "No services";
$SBS_LANG['legend_fully'] = "Fully booked";
$SBS_LANG['legend_today'] = "Today";
$SBS_LANG['legend_'] = "";

$SBS_LANG['front']['event_empty'] = "No available services on this date";
$SBS_LANG['front']['event_fully'] = "The service is fully booked";
$SBS_LANG['front']['event_past'] = "Services in the past can not be reserved";
$SBS_LANG['front']['view_events'] = "View services";

$SBS_LANG['front']['step_1'] = "Select date";
$SBS_LANG['front']['step_2'] = "Select service";
$SBS_LANG['front']['step_3'] = "Select person and time";
$SBS_LANG['front']['step_4'] = "Selected services";

$SBS_LANG['front']['services']['price'] = "Price";
$SBS_LANG['front']['services']['length'] = "Length";
$SBS_LANG['front']['services']['minutes'] = "mins";
$SBS_LANG['front']['services']['availability'] = "Availability";
$SBS_LANG['front']['services']['add'] = "Add to Cart";
$SBS_LANG['front']['services']['proceed'] = "Continue";
$SBS_LANG['front']['services']['already'] = "Already selected services";
$SBS_LANG['front']['services']['selected'] = "Selected time";
$SBS_LANG['front']['services']['pastdate'] = "Unavailable";

$SBS_LANG['front']['bf_form']  = "Booking form";
$SBS_LANG['front']['bf_name']  = "Name";
$SBS_LANG['front']['bf_phone'] = "Phone";
$SBS_LANG['front']['bf_country'] = "Country";
$SBS_LANG['front']['bf_city'] = "City";
$SBS_LANG['front']['bf_state'] = "State";
$SBS_LANG['front']['bf_zip'] = "Zip";
$SBS_LANG['front']['bf_address'] = "Address";
$SBS_LANG['front']['bf_email'] = "E-Mail";
$SBS_LANG['front']['bf_people'] = "Number of people";
$SBS_LANG['front']['bf_notes'] = "Notes";
$SBS_LANG['front']['bf_captcha'] = "Captcha";
$SBS_LANG['front']['bf_price'] = "Price";
$SBS_LANG['front']['bf_payment_method'] = "Payment method";
$SBS_LANG['front']['bf_cc_type'] = "CC type";
$SBS_LANG['front']['bf_cc_num'] = "CC number";
$SBS_LANG['front']['bf_cc_exp'] = "CC expiration date";
$SBS_LANG['front']['bf_cc_code'] = "CC security code";
$SBS_LANG['front']['bf_cc_types']['Visa'] = "Visa";
$SBS_LANG['front']['bf_cc_types']['MasterCard'] = "MasterCard";
$SBS_LANG['front']['bf_cc_types']['Maestro'] = "Maestro";
$SBS_LANG['front']['bf_cc_types']['AmericanExpress'] = "AmericanExpress";

$SBS_LANG['front']['cart']['basket'] = "Selected services";
$SBS_LANG['front']['cart']['checkout'] = "Booking Form";
$SBS_LANG['front']['cart']['summary'] = "Booking Summary";
$SBS_LANG['front']['cart']['item_name'] = "Timeslot Booking";
$SBS_LANG['front']['cart']['date'] = "Date";
$SBS_LANG['front']['cart']['start_time'] = "Start time";
$SBS_LANG['front']['cart']['end_time'] = "End time";
$SBS_LANG['front']['cart']['price'] = "Price";
$SBS_LANG['front']['cart']['remove'] = "Remove";
$SBS_LANG['front']['cart']['empty'] = "No services selected";
$SBS_LANG['front']['cart']['total'] = "Total";
$SBS_LANG['front']['cart']['passed'] = "passed";
$SBS_LANG['front']['cart']['service'] = "Service";

$SBS_LANG['front']['v_name']  = "Name is required";
$SBS_LANG['front']['v_phone'] = "Phone is required";
$SBS_LANG['front']['v_country'] = "Country is required";
$SBS_LANG['front']['v_city'] = "City is required";
$SBS_LANG['front']['v_state'] = "State is required";
$SBS_LANG['front']['v_zip'] = "Zip is required";
$SBS_LANG['front']['v_address'] = "Address is required";
$SBS_LANG['front']['v_people'] = "Number of people is required";
$SBS_LANG['front']['v_email'] = "E-Mail is required";
$SBS_LANG['front']['v_notes'] = "Notes is required";
$SBS_LANG['front']['v_payment_method'] = "Payment method is required";
$SBS_LANG['front']['v_captcha'] = "Captcha is required";
$SBS_LANG['front']['v_cc_type'] = 'Credit Card type is required';
$SBS_LANG['front']['v_cc_num'] = 'Credit Card number is required';
$SBS_LANG['front']['v_cc_exp'] = 'Credit Card expiration date is required';
$SBS_LANG['front']['v_cc_exp_month'] = 'Credit Card expiration month is required';
$SBS_LANG['front']['v_cc_exp_year'] = 'Credit Card expiration year is required';
$SBS_LANG['front']['v_cc_code'] = 'Credit Card security code is required';

$SBS_LANG['front']['msg_1'] = "Loading services...";
$SBS_LANG['front']['msg_2'] = "Loading booking form...";
$SBS_LANG['front']['msg_4'] = "Loading summary page...";
$SBS_LANG['front']['msg_5'] = "Loading calendar...";
$SBS_LANG['front']['msg_6'] = "Processing booking. Please wait...";
$SBS_LANG['front']['msg_7'] = "Reservation saved";
$SBS_LANG['front']['msg_8'] = "Reservation failed to save";

$SBS_LANG['front']['v_err_title'] = "You failed to correctly fill in the booking form:";
$SBS_LANG['front']['v_err_email'] = "E-Mail is invalid";
$SBS_LANG['front']['v_err_captcha'] = "Captcha is wrong";
$SBS_LANG['front']['v_err_payment'] = "Choose payment option";
$SBS_LANG['front']['v_err_min'] = "You must select one service at least";

$SBS_LANG['front']['button_submit'] = "Continue";
$SBS_LANG['front']['button_cancel'] = "Cancel";
$SBS_LANG['front']['button_book'] = "Book";

$SBS_LANG['front']['summary_form']  = "Booking summary";
$SBS_LANG['front']['summary_price'] = "Price";
$SBS_LANG['front']['summary_security'] = "Security deposit";
$SBS_LANG['front']['summary_tax'] = "Tax";
$SBS_LANG['front']['summary_total'] = "Total price";
$SBS_LANG['front']['summary_deposit'] = "Deposit";

$SBS_LANG['front']['pay_deposit_paypal'] = "Pay the deposit via Paypal";
$SBS_LANG['front']['pay_deposit_authorize'] = "Pay the deposit via Authorize.net&trade;";
$SBS_LANG['front']['pay_deposit_creditcard'] = "Pay the deposit with Credit card";
$SBS_LANG['front']['pay_total_paypal'] = "Pay the total price via Paypal";
$SBS_LANG['front']['pay_total_authorize'] = "Pay the total price via Authorize.net&trade;";
$SBS_LANG['front']['pay_total_creditcard'] = "Pay the total price with Credit card";

$SBS_LANG['front']['cancel_note'] = "Cancel Booking";
$SBS_LANG['front']['cancel_confirm'] = "Cancel Booking";
$SBS_LANG['front']['cancel_heading'] = "Your reservation details";
$SBS_LANG['front']['cancel_title'] = "Title";
$SBS_LANG['front']['cancel_description'] = "Description";
$SBS_LANG['front']['cancel_datetime'] = "Date/Time";
$SBS_LANG['front']['cancel_name'] = "Name";
$SBS_LANG['front']['cancel_email'] = "E-Mail";
$SBS_LANG['front']['cancel_phone'] = "Phone";
$SBS_LANG['front']['cancel_country'] = "Country";
$SBS_LANG['front']['cancel_city'] = "City";
$SBS_LANG['front']['cancel_state'] = "State";
$SBS_LANG['front']['cancel_zip'] = "Zip";
$SBS_LANG['front']['cancel_address'] = "Address";
$SBS_LANG['front']['cancel_err'][1] = "Missing parameters";
$SBS_LANG['front']['cancel_err'][2] = "Booking with such ID did not exists";
$SBS_LANG['front']['cancel_err'][3] = "Security hash did not match";
$SBS_LANG['front']['cancel_err'][4] = "Booking is already cancelled";
$SBS_LANG['front']['cancel_err'][200] = "Booking has been cancelled successful";
?>