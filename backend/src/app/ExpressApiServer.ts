import 'reflect-metadata';
import express, { Express } from 'express';
import cors from 'cors';
import { BaseRouter } from './BaseRouter';
import { Container } from 'inversify';

export class ExpressApiServer {
    private app: Express;

    constructor(
        private readonly routers: BaseRouter[],
    ) {
        this.app = express();
        this.app.set('port', process.env.PORT || 4000);
    }

    public ApplyMiddleware(): ExpressApiServer {
        this.app.use(express.json());
        this.app.use(cors());
        return this;
    }

    public AddRoutes(): ExpressApiServer {
        this.routers.forEach(router => router.DeclareRoutes());
        return this;
    }
    
    public Start(): void {
        this.app.listen(
            this.app.get('port'), 
            '0.0.0.0', 
            () => console.log('the server is running on', this.app.get('port'))
        );
    }

    public static async Bootstrap(routers: BaseRouter[]): Promise<void> {
        console.log('starting the server');
        new ExpressApiServer(routers).ApplyMiddleware().AddRoutes().Start();
    }
}
