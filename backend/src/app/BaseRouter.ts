import { Router } from "express";

export interface BaseRouter {
    Router: Router;
    Path: string;

    DeclareRoutes(): void;
}
