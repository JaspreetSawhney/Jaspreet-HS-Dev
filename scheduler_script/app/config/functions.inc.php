<?php
/**
 * Util class
 *
 * @package ebc
 * @subpackage ebc.app.config
 */
class Util
{
	function jqDateFormat($phpFormat)
	{
		$jQuery = array('d', 'dd', 'm', 'mm', 'yy');
		$php = array('j', 'd', 'n', 'm', 'Y');
		$limiters = array('.', '-', '/');
		foreach ($limiters as $limiter)
		{
			if (strpos($phpFormat, $limiter) !== false)
			{
				$_iFormat = explode($limiter, $phpFormat);
				return join($limiter, array( 
					$jQuery[array_search($_iFormat[0], $php)], 
					$jQuery[array_search($_iFormat[1], $php)], 
					$jQuery[array_search($_iFormat[2], $php)]
				));
			}
		}
		return $phpFormat;
	}
	
	function formatDate($date, $inputFormat, $outputFormat = "Y-m-d")
	{
		$limiters = array('.', '-', '/');
		foreach ($limiters as $limiter)
		{
			if (strpos($inputFormat, $limiter) !== false)
			{
				$_date = explode($limiter, $date);
				$_iFormat = explode($limiter, $inputFormat);
				$_iFormat = array_flip($_iFormat);
				break;
			}
		}
		return date($outputFormat, mktime(0, 0, 0, 
			$_date[isset($_iFormat['m']) ? $_iFormat['m'] : $_iFormat['n']], 
			$_date[isset($_iFormat['d']) ? $_iFormat['d'] : $_iFormat['j']], 
			$_date[$_iFormat['Y']]));
	}
/**
 * Format currency string
 *
 * @param decimal $price
 * @param string $currency
 * @param string $separator
 * @return string
 * @access public
 * @static
 */
	function formatCurrencySign($price, $currency, $separator = "")
	{
		if (!in_array($currency, array('USD', 'GBP', 'EUR', 'YEN')))
		{
			return $price . $separator . $currency;
		}
		switch ($currency)
		{
			case 'USD':
				$format = "$" . $separator . $price;
				break;
			case 'GBP':
				$format = "&pound;" . $separator . $price;
				break;
			case 'EUR':
				$format = "&euro;" . $separator . $price;
				break;
			case 'JPY':
				$format = "&yen;" . $separator . $price;
				break;
		}
		return $format;
	}
	
	function getTimezone($offset)
	{
		$db = array(
			'-14400' => 'America/Porto_Acre',
			'-18000' => 'America/Porto_Acre',
			'-7200' => 'America/Goose_Bay',
			'-10800' => 'America/Halifax',
			'14400' => 'Asia/Baghdad',
			'-32400' => 'America/Anchorage',
			'-36000' => 'America/Anchorage',
			'-28800' => 'America/Anchorage',
			'21600' => 'Asia/Aqtobe',
			'18000' => 'Asia/Aqtobe',
			'25200' => 'Asia/Almaty',
			'10800' => 'Asia/Yerevan',
			'43200' => 'Asia/Anadyr',
			'46800' => 'Asia/Anadyr',
			'39600' => 'Asia/Anadyr',
			'0' => 'Atlantic/Azores',
			'-3600' => 'Atlantic/Azores',
			'7200' => 'Europe/London',
			'28800' => 'Asia/Brunei',
			'3600' => 'Europe/London',
			'-39600' => 'America/Adak',
			'32400' => 'Asia/Shanghai',
			'36000' => 'Asia/Choibalsan',
			'-21600' => 'America/Chicago',
			'-25200' => 'Chile/EasterIsland',
			'-43200' => 'Pacific/Kwajalein'
		);
		if (is_null($offset) && strlen($offset) === 0)
		{
			return $db;
		}
		return array_key_exists($offset, $db) ? $db[$offset] : false;		
	}
/**
 * Print notice
 *
 * @param string $value
 * @return string
 * @access public
 * @static
 */
	function printNotice($value, $convert = true)
	{
		?>
		<div class="notice-box">
			<div class="notice-top"></div>
			<div class="notice-middle"><span class="notice-info">&nbsp;</span><?php echo $convert ? htmlspecialchars(stripslashes($value)) : stripslashes($value); ?></div>
			<div class="notice-bottom"></div>
		</div>
		<?php
	}
/**
 * Redirect browser
 *
 * @param string $url
 * @param int $http_response_code
 * @param bool $exit
 * @return void
 * @access public
 * @static
 */
	function redirect($url, $http_response_code = null, $exit = true)
	{
		//if (strtoupper(substr(PHP_OS, 0, 3)) === 'WIN')
		if (strstr($_SERVER['SERVER_SOFTWARE'], 'Microsoft-IIS'))
		{
			echo '<html><head><title></title><script type="text/javascript">window.location.href="'.$url.'";</script></head><body></body></html>';
		} else {
			$http_response_code = !is_null($http_response_code) && (int) $http_response_code > 0 ? $http_response_code : 303;
			header("Location: $url", true, $http_response_code);
		}
		if ($exit)
		{
	    	exit();
		}
	}
}
?>