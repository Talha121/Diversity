
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
export class UserGuard implements CanActivate {
    constructor(
        private authService: AuthService,
        private router: Router) { }
    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): boolean | Promise<boolean> {
        var userData = this.authService.DecodedToken;
        if (userData.role != "User") {
            this.router.navigate(['/admin/admin-dashboard']);
        }
        return true;
        
    }
}