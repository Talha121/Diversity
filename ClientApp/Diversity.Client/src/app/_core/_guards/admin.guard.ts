
import { Injectable } from "@angular/core";
import {
    ActivatedRouteSnapshot,
    CanActivate,
    Router,
    RouterStateSnapshot,
    UrlTree
} from "@angular/router";
import { AuthService } from "../_services/auth.service";

@Injectable()
export class AdminGuard implements CanActivate {
    constructor(
        private authService: AuthService,
        private router: Router) { }
    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): boolean | Promise<boolean> {
        var userData = this.authService.DecodedToken;
        if (userData.role != "Admin") {
            this.router.navigate(['/user/dashboard']);
        }
        return true;
        
    }
}