export class Error {
    
    code: number;
    message: string;

    constructor(code: number, message: string) {
        this.code = code;
        this.message = message;
    }

    static async fromResponse(res: Response): Promise<Error> {
        if (res.status === 500) return Promise.resolve(Error.unknown());
        else if (res.status === 401) return Promise.resolve(new Error(401, 'Unauthorized'));
        const data = await res.json();
        return new Error(res.status, data.message);
    }

    static unknown(): Error {
        return new Error(500, 'Internal Server Error');
    }
}