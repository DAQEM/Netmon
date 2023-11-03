export class Error {
    
    code: number;
    message: string;

    constructor(code: number, message: string) {
        this.code = code;
        this.message = message;
    }

    static fromResponse(res: Response): Promise<Error> {
        return res.json().then((data) => {
            return new Error(res.status, data.message);
        });
    }

    static unknown(): Error {
        return new Error(500, 'Internal Server Error');
    }
}