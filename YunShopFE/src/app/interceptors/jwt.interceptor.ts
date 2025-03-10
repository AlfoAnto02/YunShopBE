import { HttpInterceptorFn } from '@angular/common/http';

export const JwtInterceptor: HttpInterceptorFn = (req, next) => {
  const localToken = localStorage.getItem('token');
  if (localToken) {
    const clonedReq = req.clone({
      setHeaders: { Authorization: `Bearer ${localToken}`}
    });
    return next(clonedReq);
  }
  return next(req);
};