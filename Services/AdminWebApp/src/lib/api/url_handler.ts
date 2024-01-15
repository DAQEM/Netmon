const urlHandler = {
	getBaseUrl(url: string): string {
		if (typeof process === 'undefined') {
			return 'http://localhost:5000/api' + url;
		}
		return (process.env.BASE_URL || 'http://localhost:5000/api') + url;
	},
	getDeviceManagerUrl(url: string): string {
		return this.getBaseUrl('/device' + url);
	},
	getAccountServiceUrl(url: string): string {
		return this.getBaseUrl('/account' + url);
	},
	getUserWebAppUrl(url: string): string {
		if (typeof process === 'undefined') {
			return 'http://localhost:80' + url;
		}
		return (process.env.USER_WEB_APP_URL || 'http://localhost:80') + url;
	}
};

export default urlHandler;
